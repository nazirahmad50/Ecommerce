using Ecommerce.Entities;
using Ecommerce.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Web.ViewModels
{
    public class OrdersViewModel
    {
        public List<Order> Order { get; set; }
        public string UserID { get; set; }
        public string Status { get; set; }

        public Pager Pager { get; set; }
    }

    public class DetailsViewModel
    {
        public Order Order { get; set; }


        public ApplicationUser OrderBy { get; set; }
        public List<string> availableStatuses { get; set; }
    }
}