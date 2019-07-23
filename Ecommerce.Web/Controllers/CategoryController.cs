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


        [HttpGet]
        public ActionResult Index()
        {

            return View(); 
        }

        public ActionResult CategoriesTable(string search)
        {

            CategorySearchViewModels model = new CategorySearchViewModels
            {
                Categories = CategoriesService.Instance.GetCategories()
            };



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

            CategoriesService.Instance.SaveCategory(category); // pass the 'category' argument values to the method 'SaveCategory'

            return RedirectToAction("CategoriesTable");
        }


        [HttpGet]
        public ActionResult Edit(int id) // this 'id' name is same as the id name in the routes table
        {
            var category = CategoriesService.Instance.GetCategory(id); // call the method 'GetCategory' from categories service and set it to the variable 'categories'


            return PartialView(category);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {

            CategoriesService.Instance.UpdateCategory(category); // pass the 'category' argument values to the method 'UpdateCategory'

            return RedirectToAction("CategoriesTable"); // redirect to Index View
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            CategoriesService.Instance.DeleteCategory(id); // pass the  id value to the method 'DeleteCategory'

            return RedirectToAction("CategoriesTable");
        }
    }
}