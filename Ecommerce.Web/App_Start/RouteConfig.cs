using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ecommerce.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // default route
            routes.MapRoute(
            name: "Default",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );


            //----------------------------------------CATEGORY---------------------------------

            // Categories Table 
            routes.MapRoute(
                name: "AllCategories",
                url: "categories/all",
                defaults: new { controller = "Category", action = "CategoriesTable" }
            );

            // Create
            routes.MapRoute(
                name: "CreateCategory",
                url: "categories/create",
                defaults: new { controller = "Category", action = "Create" }
            );

            // Edit
            routes.MapRoute(
                name: "EditCategory",
                url: "categories/edit",
                defaults: new { controller = "Category", action = "Edit", id = UrlParameter.Optional }
            );

            // Delete
            routes.MapRoute(
                name: "DeleteCategory",
                url: "categories/delete",
                defaults: new { controller = "Category", action = "Delete" }
            );

            //----------------------------------------PRODUCT---------------------------------

            // Products Table 
            routes.MapRoute(
                name: "AllProducts",
                url: "products/all",
                defaults: new { controller = "Product", action = "ProductsTable" }
            );

            // Create
            routes.MapRoute(
                name: "CreateProduct",
                url: "products/create",
                defaults: new { controller = "Product", action = "Create" }
            );

            // Edit
            routes.MapRoute(
                 name: "EditProduct",
                 url: "products/edit",
                  defaults: new { controller = "Product", action = "Edit", id = UrlParameter.Optional }
              );

            // Delete
            routes.MapRoute(
                 name: "DeleteProduct",
                 url: "products/delete",
                 defaults: new { controller = "Product", action = "Delete" }
             );


            //----------------------------------------SHARED---------------------------------

            // UploadImage
            routes.MapRoute(
                 name: "UploadImage",
                 url: "shared/uploadimage",
                 defaults: new { controller = "Shared", action = "UploadImage" }
             );



        }
    }
}
