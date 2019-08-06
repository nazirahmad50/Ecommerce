using Ecommerce.Services;
using Ecommerce.Web.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class OrderController : Controller
    {


        // need both sign in manager and user manager to get user data
        // we cannot access the User table in database directly like using context so we have to use SignInManager and UserManager
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OrdersTable(string userId, string status, int? pageNo)
       {
            // if pageNo has a value and its value is greater than 0 then set mode.pageNo to that pageNo value
            // else if pageNo doesnt have value then set 'model.pageNo'  to 1
            // else if pageNo value is not greater than 0 then set 'model.pageNo' to 1
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            var totalRecords = OrdersService.Instance.GetOrdersCount(userId, status); // get total Orders
            int pageSizeConfig = int.Parse(ConfigurationService.Instance.GetConfig("PageSize").Value); // get the page size value from config key called PageSize

            OrdersViewModel model = new OrdersViewModel
            {
                Order = OrdersService.Instance.SearchProducts(userId, status, pageNo.Value, pageSizeConfig),
                UserID = userId,
                Status = status
            };

            if (model.Order != null)
            {

                // pass the total items, pageNo and page size to calculate pagination values
                model.Pager = new Pager(totalRecords, pageNo.Value, pageSizeConfig);


                // used 'PartialView' becuase we are trying to render this actions View in the Index View, so when we use 'PartialView' it will only render the elements in the Index View from this actions View
                return PartialView(model);
            }
            else
            {
                return HttpNotFound();
            }

        }

        public ActionResult Details(int ID)
        {

            DetailsViewModel model = new DetailsViewModel();

            model.Order = OrdersService.Instance.GetOrderById(ID); // get the order by id
      
            if (model.Order != null) // if Order object is not null
            {
                model.OrderBy = UserManager.FindById(model.Order.UserID); // find the user based on their id and set it to 'OrderBy'
            }
            model.availableStatuses = new List<string>() { "Pending", "In Progress", "Delivered" }; // create string list with different statuses

            return View(model);

        }

        public JsonResult ChangeStatus(string status,int ID)
        {

            JsonResult result = new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet, // need this to allow json

                // create data obejct and create inside it propert called 'success'
                // 'IsUpdateOrderStatus' will return to check if status has changed
                Data = new { success = OrdersService.Instance.IsUpdateOrderStatus(status, ID) } 
            };


            return result;

        }

    }
}