using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entities
{
    public class Category : BaseEntity
    {
        public string ImageURL { get; set; }

        public List<Product> Products { get; set; } // there can be multiple products in a category
    }
}
