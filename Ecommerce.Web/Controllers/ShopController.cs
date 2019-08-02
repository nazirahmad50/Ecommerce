using Ecommerce.Services;
using Ecommerce.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class ShopController : Controller
    {

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




        public ActionResult Checkout()
        {
            CheckoutViewModel model = new CheckoutViewModel();

            // 'request' Enables ASP.NET to read the HTTP values sent by a client during a Web request 
            var cartProductCokkie = Request.Cookies["cartProducts"]; // so we can get cookies based on their name through the 'Request' prop

            if (cartProductCokkie != null)
            {
                // get the values from the cookie and split them at '-'
                // then select those products and convert them to int and return them as list
               model.CartProductIds =  cartProductCokkie.Value.Split('-').Select(x => int.Parse(x)).ToList(); ;

                model.CartProducts = ProductsService.Instance.GetProducts(model.CartProductIds); // get products from database based on the list of product ids


            }

            return View(model);
        }
    }
}