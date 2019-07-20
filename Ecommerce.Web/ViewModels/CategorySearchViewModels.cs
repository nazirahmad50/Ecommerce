using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Web.ViewModels
{
    public class CategorySearchViewModels
    {
        public List<Category> Categories { get; set; }
        public string SearchTerm { get; set; }
    }
}