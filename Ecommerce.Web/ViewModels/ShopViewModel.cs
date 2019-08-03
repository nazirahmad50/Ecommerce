using Ecommerce.Entities;
using Ecommerce.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Web.ViewModels
{
    public class CheckoutViewModel
    {
        public List<Product> CartProducts { get; set; }

        public List<int> CartProductIds { get; set; }

        public ApplicationUser User { get; set; }
    }

    public class ShopViewModel
    {
        public int MaximumPrice { get; set; }
        public int? SortBy { get; set; }
        public int? categoryId { get; set; }
        public string searchTerm { get; set; }


        public List<Product> Products { get; set; }
        public List<Category> FeaturedCategories { get; set; }

        public Pager Pager { get; set; }
    }

    public class FilterProductsViewModel {


        public int SortBy { get; set; }
        public int? categoryId { get; set; }
        public string searchTerm { get; set; }

        public List<Product> Products { get; set; }

        public Pager Pager { get; set; }


    }
}