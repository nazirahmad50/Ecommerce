using Ecommerce.Entities;
using Ecommerce.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class ConfigurationController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ConfigurationTable(string search)
        {
            var configs = ConfigurationService.Instance.GetConfigs();

            if (!string.IsNullOrEmpty(search))
            {
                configs = configs.Where(x => x.Key != null && x.Key.ToLower().Contains(search.ToLower())).ToList();
            }


            return PartialView(configs);
        }

        [HttpGet]
        public ActionResult Edit(string key) 
        {
            var config = ConfigurationService.Instance.GetConfig(key); 


            return PartialView(config);
        }

        [HttpPost]
        public ActionResult Edit(Config config)
        {

            ConfigurationService.Instance.UpdateConfigs(config); 

            return RedirectToAction("ConfigurationTable"); 
        }

    }
}