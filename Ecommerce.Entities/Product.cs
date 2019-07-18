using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entities
{
    public class Product : BaseEntity
    {

        public decimal Price { get; set; }

        // if we mark an object as 'virtual' this tells entity framework that we want to use data from that object as well
        public virtual Category Category { get; set; } // Category object and Product can only have one category
    }
}
