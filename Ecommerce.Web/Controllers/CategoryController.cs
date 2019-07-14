using Ecommerce.Services;
using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class CategoryController : Controller
    {

        readonly CategoriesService categoriesService = new CategoriesService(); // create an object of 'CategoriesService' 

        [HttpGet]
        public ActionResult Index()
        {
            var categories = categoriesService.GetCategories(); // call the 'GetCategories' from categories service and set it to the variable 'categories'

            return View(categories); // pass the categories list to the view
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {

            categoriesService.SaveCategory(category); // pass the 'category' argument values to the method 'SaveCategory'

            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Edit(int id) // this 'id' name is same as the id name in the routes table
        {
            var category = categoriesService.GetCategory(id); // call the method 'GetCategory' from categories service and set it to the variable 'categories'


            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {

            categoriesService.UpdateCategory(category); // pass the 'category' argument values to the method 'UpdateCategory'

            return RedirectToAction("Index"); // redirect to Index View
        }


        [HttpGet]
        public ActionResult Delete(int id) // this 'id' name is same as the id name in the routes table
        {
            var category = categoriesService.GetCategory(id); // call the 'GetCategories' from categories service and set it to the variable 'categories'


            return View(category);
        }

        [HttpPost]
        public ActionResult Delete(Category category)
        {
            categoriesService.DeleteCategory(category.ID); // pass the 'category' id value to the method 'DeleteCategory'

            return RedirectToAction("Index");
        }
    }
}