using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class SharedController : Controller
    {
        public JsonResult UploadImage() // this method will return 'Json' where as 'ActionResult' returns HTML
        {

            JsonResult result = new JsonResult(); // create new json result object
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet; // MVC doesnt allow for Json without this json request behavior

            try
            {
                var file = Request.Files[0]; // this 'Request' will get the file from that FormData which we appended a file into using ajax

                // create the name of the file
                // 'Guid' will geenrate a 128 bit integer for the name of the file, this is so that when we pass a file with the same name again it wont give us an error
                // and add the extension of that file at the end
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName); 

                var path = Path.Combine(Server.MapPath("~/Content/images/"), fileName); // create a path to store the file

                file.SaveAs(path); //save the file in the path

                // after the '=' we are creating an anonymous object, so we can name those properties anthing we want
                // imageUrl will be from the content/images and the name of the file
                result.Data = new { Success = true, ImageUrl = string.Format("/Content/images/{0}", fileName) };

                //var newImage = new Image() { Name = fileName };

                //if (!ImageService.)
                //{

                //}
                //else
                //{

                //}
            }
            catch (Exception ex)
            {
                // if there is an error then success will be false and return the error message as well
                result.Data = new { Success = false, Message = ex.Message};

            }

            return result; // return that json result object
        }
    }
}