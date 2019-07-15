using Ecommerce.Entities;
using Ecommerce.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class ProductController : Controller
    {
        ProductsService productsService = new ProductsService();

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductsTable(string search) // the value for the 'search' parameter will be recieved through ajax
        {
            var products = productsService.GetProducts();

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
            return PartialView();
        }

        // this action method is called when we click on the save button
        [HttpPost]
        public ActionResult Create(Product product)
        {

            productsService.SaveProduct(product); // pass the 'category' argument values to the method 'SaveCategory'

            return RedirectToAction("ProductsTable");
        }
    }
}