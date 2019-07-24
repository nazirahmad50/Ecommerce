using Ecommerce.Services;
using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Web.ViewModels;
using System.Data.Entity;

namespace Ecommerce.Web.Controllers
{
    public class CategoryController : Controller
    {


        [HttpGet]
        public ActionResult Index()
        {

            return View(); 
        }

        public ActionResult CategoriesTable(string search, int? pageNo)
        {
            // if pageNo has a value and its value is greater than 0 then set pageNo to that pageNo value
            // else if pageNo doesnt have value then set 'model.pageNo'  to 1
            // else if pageNo value is not greater than 0 then set 'model.pageNo' to 1
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            var totalRecords = CategoriesService.Instance.GetCategoriesCount(search); // get total categories

            // get categories and set it in model
            CategorySearchViewModels model = new CategorySearchViewModels
            {
                Categories = CategoriesService.Instance.GetCategories(search, pageNo.Value)
            };

            model.SearchTerm = search;

            if (model.Categories != null)
            {
                int pageSize = int.Parse(ConfigurationService.Instance.GetConfig("PageSize").Value); // get the page size value from config key called PageSize

                // pass the total items, pageNo and page size to calculate pagination values
                model.Pager = new Pager(totalRecords, pageNo,pageSize); 


                return PartialView("_CategoriesTable", model);
            }
            else
            {
                return HttpNotFound();
            }
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