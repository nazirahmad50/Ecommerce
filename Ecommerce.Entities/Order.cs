using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entities
{
    public class Order
    {
        public int ID { get; set; }
        public string UserID { get; set; }

        public DateTime OrderdAt { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }

        // if we mark an object as 'virtual' this tells entity framework that we want to use data from that object as well
        public virtual List<OrderItem> OrderItem { get; set; }

    }
}
