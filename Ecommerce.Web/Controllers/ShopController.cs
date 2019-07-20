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
        ProductsService productsService = new ProductsService();

        public ActionResult Checkout()
        {
            CheckoutViewModels model = new CheckoutViewModels();

            // 'request' Enables ASP.NET to read the HTTP values sent by a client during a Web request 
            var cartProductCokkie = Request.Cookies["cartProducts"]; // so we can get cookies based on their name through the 'Request' prop

            if (cartProductCokkie != null)
            {
                // get the values from the cookie and split them at '-'
                // then select those products and convert them to int and return them as list
               model.CartProductIds =  cartProductCokkie.Value.Split('-').Select(x => int.Parse(x)).ToList(); ;

                model.CartProducts = productsService.GetProducts(model.CartProductIds); // get products from database based on the list of product ids


            }

            return View(model);
        }
    }
}