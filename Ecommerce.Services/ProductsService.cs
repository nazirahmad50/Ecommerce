using Ecommerce.Database;
using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace Ecommerce.Services // services are used to communicate between the Web and the Database
{
    public class ProductsService
    {

        

        #region Singleton
        public static ProductsService Instance
        {
            get
            {
                // 'PrivateInMemoryObject' doesnt exist in memory then create 'PrivateInMemoryObject' obejct
                if (instance == null) instance = new ProductsService();

                // if its not null just return 'PrivateInMemoryObject'
                return instance;
            }
        }

        private static ProductsService instance { get; set; }

        private ProductsService()
        {

        }
        #endregion


        public List<Product> GetFeaturedProducts()
        {
           
            using (var context = new CBContext())
            {

                return context.Products.Where(x => x.isFeatured && x.ImageURL != null).ToList();           

            }


        }

        public void SaveProduct(Product product)
        {
            using (var context = new CBContext()) // create an object of 'CBContext' 
            {
                // keep category unchanged becasue we dont want to create another category with every product that we add
                context.Entry(product.Category).State = System.Data.Entity.EntityState.Unchanged; 

                context.Products.Add(product); // add 'category' argument to the database 'Categories', will be stored in memory not in database yet
                context.SaveChanges(); // will save it into the database
            }


        }


        public List<Product> GetAllProducts()
        {

            using (var context = new CBContext())
            {

                return context.Products.Include(x => x.Category).ToList();


            }

        }

        public int GetProductsCount()
        {

            using (var context = new CBContext())
            {

                return context.Products.Count();


            }

        }

        /// <summary>
        /// Get all products with Category included
        /// </summary>
        public List<Product> GetProducts(string search, int pageNo)
        {
            int pageSize = int.Parse(ConfigurationService.Instance.GetConfig("PageSize").Value); // get the page size from the config key

            using (var context = new CBContext())
            {
                if (!string.IsNullOrEmpty(search)) // check if 'search' param holds a value
                {
                    return context.Products
                        .Where(x => x.Name != null && x.Name.ToLower().Contains(search.ToLower()))
                        .OrderBy(x => x.ID)
                        .Skip((pageNo - 1) * pageSize)
                        .Take(pageSize)
                        .Include(x => x.Category)
                        .ToList();
                }
                else // return all categories 
                {
                    return context.Products
                        .OrderBy(x => x.ID)
                        .Skip((pageNo - 1) * pageSize)
                        .Take(pageSize)
                        .Include(x => x.Category)
                        .ToList();

                }


            }



        }

        /// <summary>
        /// Get Products that are added to the cart
        /// </summary>
        public List<Product> GetProducts(List<int> IDs)
        {
            using (var context = new CBContext())
            {
                return context.Products.Where(x => IDs.Contains(x.ID)).ToList(); // return products where param 'IDs' contain product 'id' as a list

            }

        }


        public Product GetProduct(int ID)
        {
            using (var context = new CBContext())
            {
                return context.Products.Where(x => x.ID == ID).Include(x => x.Category).FirstOrDefault(); 

            }

        }

  

        public void UpdatepProduct(Product product)
        {
            using (var context = new CBContext())
            {
                context.Entry(product).State = System.Data.Entity.EntityState.Modified; // updates the categories database
                context.SaveChanges(); 

            }

        }

        public void DeleteProduct(int ID)
        {
            using (var context = new CBContext())
            {
                var productToRemove = context.Products.Find(ID); // find the category to remove based on the ID parameter

                context.Products.Remove(productToRemove); // remove that category
                context.SaveChanges();

            }

        }
    }
}
