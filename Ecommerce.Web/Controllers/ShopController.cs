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

        public ActionResult Index(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryId, int? sortBy = 1)
        {
            ShopViewModel model = new ShopViewModel {

                FeaturedCategories = CategoriesService.Instance.GetFeaturedCategories(),
                MaximumPrice = ProductsService.Instance.GetMaximumPrice(),
                Products = ProductsService.Instance.SearchProducts(searchTerm, minimumPrice, maximumPrice, categoryId, sortBy),
                SortBy = sortBy.Value
               
            };


            return View(model);
        }


        // List of products partial View
        public ActionResult FilterProducts(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryId, int? sortBy = 1)
        {
            FilterProductsViewModel model = new FilterProductsViewModel
            {
                Products = ProductsService.Instance.SearchProducts(searchTerm, minimumPrice, maximumPrice, categoryId, sortBy),
             
            };


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