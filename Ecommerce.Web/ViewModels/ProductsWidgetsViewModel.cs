using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Web.ViewModels
{
    public class ProductsWidgetsViewModel
    {

        public List<Product> Products { get; set; }

        public bool isLatestProduct { get; set; }
    }
}