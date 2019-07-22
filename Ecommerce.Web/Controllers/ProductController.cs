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

        public ActionResult ProductsTable(string search) // the value for the 'search' parameter will be recieved through ajax
       {
            var products = ProductsService.Instance.GetProducts();

            if (!string.IsNullOrEmpty(search)) // if 'search' parameter is not null or empty
            {
                products = products.Where(x => x.Name.ToLower().Contains(search.ToLower())).ToList(); // get the product from database where its name is equal to the 'search' parameter

            }

            // used 'PartialView' becuase we are trying to render this actions View in the Index View, so when we use 'PartialView' it will only render the elements in the Index View from this actions View
            return PartialView(products); 
        }

        [HttpGet]
        public ActionResult Create()
        {


            var categories = CategoriesService.Instance.GetCategories(); // get a list of all categories

            return PartialView(categories);
        }

        // this action method is called when we click on the save button
        [HttpPost]
        public ActionResult Create(ProductViewModels model)
        {


            var newProduct = new Product
            {
                // set all the properties in the 'ProductViewModels' to the properties in the 'Product' (entity)
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Category = CategoriesService.Instance.GetCategory(model.CategoryId)
            }; 

            ProductsService.Instance.SaveProduct(newProduct); 

            return RedirectToAction("ProductsTable");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {

            var product = ProductsService.Instance.GetProduct(id);

            return PartialView(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {

            ProductsService.Instance.UpdatepProduct(product); 

            return RedirectToAction("ProductsTable");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {

            ProductsService.Instance.DeleteProduct(id); 

            return RedirectToAction("ProductsTable");
        }
    }
}