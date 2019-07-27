using Ecommerce.Services;
using Ecommerce.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class WidgetsController : Controller
    {
        // GET: Widgets
        public ActionResult Products(bool isLatestproduct)
        {
            ProductsWidgetsViewModel model = new ProductsWidgetsViewModel
            {
                isLatestProduct = isLatestproduct
            };

            if (isLatestproduct)
            {
                model.Products = ProductsService.Instance.GetLatestProducts(4);

            }
            else // else get products by category
            {

                model.Products = ProductsService.Instance.GetProductsByCategory(1, 8);

            }


            return PartialView(model);
        }
    }
}