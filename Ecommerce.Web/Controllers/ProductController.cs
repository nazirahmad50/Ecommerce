using Ecommerce.Entities;
using Ecommerce.Services;
using Ecommerce.Web.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class ProductController : Controller
    {


        public ActionResult Index()
        {
            return View();
        }

        // the value for the 'search' parameter will be recieved through ajax
        public ActionResult ProductsTable(string search, int? pageNo) // '?' means that the var pageNo can be nullable
        {


            // if pageNo has a value and its value is greater than 0 then set mode.pageNo to that pageNo value
            // else if pageNo doesnt have value then set 'model.pageNo'  to 1
            // else if pageNo value is not greater than 0 then set 'model.pageNo' to 1
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            var totalRecords = ProductsService.Instance.GetProductsCount(); // get total Products


            ProductTablesViewModels model = new ProductTablesViewModels
            {

                Products = ProductsService.Instance.GetProducts(search, pageNo.Value)

            };

            model.SearchTerm = search;

            if (model.Products != null)
            {
                int pageSize = int.Parse(ConfigurationService.Instance.GetConfig("PageSize").Value); // get the page size value from config key called PageSize

                // pass the total items, pageNo and page size to calculate pagination values
                model.Pager = new Pager(totalRecords, pageNo, pageSize);


                // used 'PartialView' becuase we are trying to render this actions View in the Index View, so when we use 'PartialView' it will only render the elements in the Index View from this actions View
                return PartialView(model);
            }
            else
            {
                return HttpNotFound();
            }
        }



        [HttpGet]
        public ActionResult Create()
        {


            var categories = CategoriesService.Instance.GetAllCategories(); // get a list of all categories

            return PartialView(categories);
        }

        // this action method is called when we click on the save button
        [HttpPost]
        public ActionResult Create(ProductViewModels model)
        {
            // Checks if there is any issues with the data posted to the server, based on the data annotations added to the properties of your model.
            if (ModelState.IsValid)
            {
                var newProduct = new Product
                {
                    // set all the properties in the 'ProductViewModels' to the properties in the 'Product' (entity)
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    ImageURL = model.ImageURL,
                    isFeatured = model.isFeatured,
                    Category = CategoriesService.Instance.GetCategory(model.CategoryId)
                };

                ProductsService.Instance.SaveProduct(newProduct);

                return RedirectToAction("ProductsTable");

            }
            else
            {
                return new HttpStatusCodeResult(500);
            }

        }


        [HttpGet]
        public ActionResult Edit(int id)
        {

            var product = ProductsService.Instance.GetProduct(id);



            ProductViewModels model = new ProductViewModels
            {
                ID = product.ID,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageURL = product.ImageURL,
                isFeatured = product.isFeatured,
                CategoryId = product.Category != null ? product.Category.ID : 0

            };

            model.AvailableCategories = CategoriesService.Instance.GetAllCategories();


            return PartialView(model);
        }





        [HttpPost]
        public ActionResult Edit(ProductViewModels model)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = ProductsService.Instance.GetProduct(model.ID);

                existingProduct.Name = model.Name;
                existingProduct.Description = model.Description;
                existingProduct.Price = model.Price;
                existingProduct.Category = CategoriesService.Instance.GetCategory(model.CategoryId);
                existingProduct.isFeatured = model.isFeatured;

                if (!string.IsNullOrEmpty(model.ImageURL))
                {
                    existingProduct.ImageURL = model.ImageURL;

                }



                ProductsService.Instance.UpdatepProduct(existingProduct);

                return RedirectToAction("ProductsTable");
            }
            else
            {
                return new HttpStatusCodeResult(500);

            }

        }

        [HttpPost]
    public ActionResult Delete(int id)
    {

        ProductsService.Instance.DeleteProduct(id);

        return RedirectToAction("ProductsTable");
    }



        [HttpGet]
        public ActionResult Details(int id)
        {

            var product = ProductsService.Instance.GetProduct(id);


            return View(product);
        }
    }
}