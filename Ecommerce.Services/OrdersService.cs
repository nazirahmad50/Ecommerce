using Ecommerce.Database;
using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Ecommerce.Services
{
    public class OrdersService
    {
        #region Singleton
        public static OrdersService Instance
        {
            get
            {
                // 'PrivateInMemoryObject' doesnt exist in memory then create 'PrivateInMemoryObject' obejct
                if (instance == null) instance = new OrdersService();

                // if its not null just return 'PrivateInMemoryObject'
                return instance;
            }
        }



        private static OrdersService instance { get; set; }

        private OrdersService()
        {

        }
        #endregion


        public List<Order> SearchProducts(string userId, string status, int? pageNo, int pageSize)
        {

            using (var context = new CBContext())
            {

                var orders = context.Order.ToList();


                if (!string.IsNullOrEmpty(userId)) 
                {
                    return orders = orders
                        .Where(x => x.UserID != null && x.UserID.ToLower().Contains(userId.ToLower()))
                        .OrderBy(x => x.UserID)
                        .Skip((pageNo.Value - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
                }

                if (!string.IsNullOrEmpty(status)) // check if 'search' param holds a value
                {
                    return orders = orders
                        .Where(x => x.Status != null && x.Status.ToLower().Contains(status.ToLower()))
                        .OrderBy(x => x.Status)
                        .Skip((pageNo.Value - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
                }

                return orders
                    .Skip((pageNo.Value - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();


            }

        }

        public int GetOrdersCount(string userId, string status)
        {

            using (var context = new CBContext())
            {

                var orders = context.Order.ToList();


                if (!string.IsNullOrEmpty(userId))
                {
                    orders = orders
                        .Where(x => x.UserID != null && x.UserID.ToLower().Contains(userId.ToLower()))
                        .ToList();
                }

                if (!string.IsNullOrEmpty(status)) // check if 'search' param holds a value
                {
                    orders = orders
                        .Where(x => x.Status != null && x.UserID.ToLower().Contains(status.ToLower()))
                        .ToList();
                }

                return orders.Count;


            }

        }

        public Order GetOrderById(int ID)
        {
            using (var context = new CBContext())
            {

                return context.Order
                    .Where(x=> x.ID == ID) // get the Order based on the param ID
                    .Include(x=>x.OrderItem) // include the 'OrderItem' list as well, so we can access the 'OrderItem' values as well
                    .Include("OrderItem.Product") // and include the 'Product' object which is inside the 'OrderItem' class, so we can access the 'Product' object values as well
                    .FirstOrDefault();


            }
        }


        public bool IsUpdateOrderStatus(string status, int ID)
        {
            using (var context = new CBContext())
            {
                var order = context.Order.Find(ID); // find the order by id

                order.Status = status; // set teh status of that order to teh param stats

                return context.SaveChanges() > 0; // return if the saved changes is more than 0
          

            }
        }

    }
}
