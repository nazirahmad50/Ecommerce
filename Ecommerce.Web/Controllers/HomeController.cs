using Ecommerce.Services;
using Ecommerce.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            HomeViewModels model = new HomeViewModels
            {
                FeaturedCategories = CategoriesService.Instance.GetFeaturedCategories(), // set the categories list in the home view model to the categories receieved from database
                FeaturedProducts = ProductsService.Instance.GetFeaturedProducts()
            }; 

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}