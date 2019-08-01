using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entities
{
    public class Product : BaseEntity
    {
        [Range(1, 100000)]
        public decimal Price { get; set; }

        // if we mark an object as 'virtual' this tells entity framework that we want to use data from that object as well
        public virtual Category Category { get; set; } // Category object and Product can only have one category
        public int CategoryID { get; set; }


        public string ImageURL { get; set; }

        public bool isFeatured { get; set; }

    }
}
