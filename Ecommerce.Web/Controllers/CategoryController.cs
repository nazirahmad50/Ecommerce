using Ecommerce.Services;
using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Web.ViewModels;

namespace Ecommerce.Web.Controllers
{
    public class CategoryController : Controller
    {

        readonly CategoriesService categoriesService = new CategoriesService(); // create an object of 'CategoriesService' 

        [HttpGet]
        public ActionResult Index()
        {

            return View(); 
        }

        public ActionResult CategoriesTable(string search)
        {

            CategorySearchViewModels model = new CategorySearchViewModels();

             model.Categories = categoriesService.GetCategories();
            


            if (!string.IsNullOrEmpty(search))
            {
                model.SearchTerm = search;
                model.Categories = model.Categories.Where(x => x.Name != null && x.Name.ToLower().Contains(model.SearchTerm.ToLower())).ToList();
            }


            return PartialView("_CategoriesTable", model);
        }

        [HttpGet]
        public ActionResult Create()
        {


            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {

            categoriesService.SaveCategory(category); // pass the 'category' argument values to the method 'SaveCategory'

            return RedirectToAction("CategoriesTable");
        }


        [HttpGet]
        public ActionResult Edit(int id) // this 'id' name is same as the id name in the routes table
        {
            var category = categoriesService.GetCategory(id); // call the method 'GetCategory' from categories service and set it to the variable 'categories'


            return PartialView(category);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {

            categoriesService.UpdateCategory(category); // pass the 'category' argument values to the method 'UpdateCategory'

            return RedirectToAction("CategoriesTable"); // redirect to Index View
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            categoriesService.DeleteCategory(id); // pass the  id value to the method 'DeleteCategory'

            return RedirectToAction("CategoriesTable");
        }
    }
}