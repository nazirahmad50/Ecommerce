using Ecommerce.Database;
using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services
{
    public class ShopService
    {
        #region Singleton
        public static ShopService Instance
        {
            get
            {
                // 'PrivateInMemoryObject' doesnt exist in memory then create 'PrivateInMemoryObject' obejct
                if (instance == null) instance = new ShopService();

                // if its not null just return 'PrivateInMemoryObject'
                return instance;
            }
        }



        private static ShopService instance { get; set; }

        private ShopService()
        {

        }
        #endregion


        public void SaveOrders(Order order)
        {
            using (var context = new CBContext())
            {
                context.Order.Add(order);
                context.SaveChanges();

            }
        }

    }
}
