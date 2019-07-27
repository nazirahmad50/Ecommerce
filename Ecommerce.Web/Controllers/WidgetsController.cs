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
        public ActionResult Products(bool isLatestproduct, int? categoryId = 0)
        {
            ProductsWidgetsViewModel model = new ProductsWidgetsViewModel
            {
                isLatestProduct = isLatestproduct
            };

            if (isLatestproduct) // show latest (New Products) products
            {
                model.Products = ProductsService.Instance.GetProductsByCategoryHome(1, 8);

            }
            else if (categoryId.HasValue && categoryId > 0) // show related products
            {
                model.Products = ProductsService.Instance.GetProductsByCategoryDetails(categoryId.Value, 4);

            }
            else // else get products by category
            {

                model.Products = ProductsService.Instance.GetProductsByCategoryHome(1, 8);

            }


            return PartialView(model);
        }
    }
}
