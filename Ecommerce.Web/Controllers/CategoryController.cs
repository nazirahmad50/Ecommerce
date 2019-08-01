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
            CategorySearchViewModel model = new CategorySearchViewModel
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


            EditCategoryViewModel model = new EditCategoryViewModel
            {
                ID = category.ID,
                Name = category.Name,
                Description = category.Description,
                ImageURL = category.ImageURL,
                isFeatured = category.isFeatured
            };

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Edit(EditCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {

                var existingCategory = CategoriesService.Instance.GetCategory(model.ID);

                existingCategory.Name = model.Name;
                existingCategory.Description = model.Description;
                existingCategory.isFeatured = model.isFeatured;

                if (!string.IsNullOrEmpty(model.ImageURL))
                { // only add an image to category when the imageUrl is not empty

                    existingCategory.ImageURL = model.ImageURL;

                }


                CategoriesService.Instance.UpdateCategory(existingCategory); // update category


                return RedirectToAction("CategoriesTable"); // redirect to Index View
            }
            else
            {
                return new HttpStatusCodeResult(500);

            }
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            CategoriesService.Instance.DeleteCategory(id); // pass the  id value to the method 'DeleteCategory'

            return RedirectToAction("CategoriesTable");
        }
    }
}