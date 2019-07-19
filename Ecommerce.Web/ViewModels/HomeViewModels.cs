using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Web.ViewModels
{
    public class HomeViewModels // view models are used when you want several different models to be used such as having category and products in one model
    {
        public List<Category> FeaturedCategories { get; set; }
        public List<Product> FeaturedProducts { get; set; }
    }
}