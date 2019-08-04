using Ecommerce.Entities;
using Ecommerce.Services;
using Ecommerce.Web.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class ShopController : Controller
    {

        // need both sign in manager and user manager to get user data
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryId, int? pageNo, int? sortBy = 1)
        {

            ShopViewModel model = new ShopViewModel();

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1; // validate 'pageNo' value


            model.FeaturedCategories = CategoriesService.Instance.GetFeaturedCategories(); // get featured categories
            model.MaximumPrice = ProductsService.Instance.GetMaximumPrice(); // get maximum price of products
            model.Products = ProductsService.Instance.SearchProducts(searchTerm, minimumPrice, maximumPrice, categoryId, sortBy, pageNo.Value, 10);
            model.SortBy = sortBy.Value;
            model.categoryId = categoryId;
            model.searchTerm = searchTerm;


            int totalCount = ProductsService.Instance.SearchProductsCount(searchTerm, minimumPrice, maximumPrice, categoryId, sortBy); // get total count of search Products

            model.Pager = new Pager(totalCount, pageNo); // instantiate Pager


            return View(model);
        }


        // List of products partial View
        // the View of this action will only include the list of products
        public ActionResult FilterProducts(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryId, int? pageNo, int? sortBy = 1)
        {
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1; // validate 'pageNo' value


            FilterProductsViewModel model = new FilterProductsViewModel
            {
                Products = ProductsService.Instance.SearchProducts(searchTerm, minimumPrice, maximumPrice, categoryId, sortBy, pageNo.Value, 10),
                SortBy = sortBy.Value,
                categoryId = categoryId,
                searchTerm = searchTerm
            };

            int totalCount = ProductsService.Instance.SearchProductsCount(searchTerm, minimumPrice, maximumPrice, categoryId, sortBy); // get total count of search Products
            model.Pager = new Pager(totalCount, pageNo); // instantiate Pager


            return PartialView(model);
        }



        [Authorize] // only logged in user can access this action
        public ActionResult Checkout()
        {
            CheckoutViewModel model = new CheckoutViewModel();

            // 'request' Enables ASP.NET to read the HTTP values sent by a client during a Web request 
            var cartProductCokkie = Request.Cookies["cartProducts"]; // so we can get cookies based on their name through the 'Request' prop

            if (cartProductCokkie != null && !string.IsNullOrEmpty(cartProductCokkie.Value))
            {
                // get the values from the cookie and split them at '-'
                // then select those products and convert them to int and return them as list
               model.CartProductIds =  cartProductCokkie.Value.Split('-').Select(x => int.Parse(x)).ToList(); ;

                model.CartProducts = ProductsService.Instance.GetProducts(model.CartProductIds); // get products from database based on the list of product ids

                // first get the id of the current user logged in which is done through 'GetUserId()'
                // then find that current user id in the database through the 'UserManager' and set it to prop 'User'
                model.User = UserManager.FindById(User.Identity.GetUserId());
            }

            return View(model);
        }


        // PlaceOrder button method
        public JsonResult PlaceOrder(string productIds)  // this method will return 'Json' where as 'ActionResult' returns HTML
        {
            JsonResult result = new JsonResult(); // create JsonResult obejct
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet; // MVC doesnt allow for Json without this json request behavior

            if (!string.IsNullOrEmpty(productIds)) // if productIds not null or empty
            {

                // split the 'pQuantities' at '-' and then parse each one of them to int then convert them to list
                //e.g. before split 1-2-3, after split 1,2,3 then select those split string values and parse them to int
                var pQuantities = productIds.Split('-').Select(x => int.Parse(x)).ToList();

                // 1. get a list of distinct products from 'pQuantities'
                // 2. find those distinct products in database
                var boughtProducts = ProductsService.Instance.GetProducts(pQuantities.Distinct().ToList());


                Order newOrder = new Order();
                newOrder.UserID = User.Identity.GetUserId(); // set the user id of currently logged in user
                newOrder.OrderdAt = DateTime.Now; // set current date and time
                newOrder.Status = "Pending"; // by default set status to pending
                newOrder.TotalAmount = boughtProducts.Sum(x => x.Price * pQuantities.Where(c => c == x.ID).Count()); // calculate total price of bougth products

                newOrder.OrderItem = new List<OrderItem>(); // create a new list of OrderItem

                // 'AddRange()' is much faster at adding colelction of items to an array than 'Add()'
                // 1. First it sets the 'ProductID' in 'OrderItem' to bouth products id that is selected  
                // 2. Then it adds those bougth products that has their id equal to the 'ProductID' in the 'OrderItem' class
                // the same is done for Quantity
                newOrder.OrderItem.AddRange(boughtProducts.Select(x => new OrderItem() { ProductID = x.ID, Quantity = pQuantities.Where(c => c == x.ID).Count() }));


                ShopService.Instance.SaveOrders(newOrder); // save to database

                // 'Data' is a property object inside the 'JsonResult' class we can use it to create props inside it and manage it
                result.Data = new { success = true }; // create prop 'success' and set it to true
            }
            else // if productIds is null or empty
            {
                result.Data = new { success = false }; // create prop 'success' and set it to false as productIds were null or empty

            }

            return result;
        }
    }
}