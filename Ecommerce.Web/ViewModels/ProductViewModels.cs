using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Web.ViewModels
{
    public class ProductViewModels
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public int CategoryId { get; set; }

    }
}