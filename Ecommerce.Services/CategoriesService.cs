using Ecommerce.Database;
using Ecommerce.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace Ecommerce.Services // services are used to communicate between the Web and the Database
{
    public class CategoriesService
    {

        #region Singleton
        public static CategoriesService Instance
        {
            get {
                // 'PrivateInMemoryObject' doesnt exist in memory then create 'PrivateInMemoryObject' obejct
                if (instance == null) instance = new CategoriesService();

                // if its not null just return 'PrivateInMemoryObject'
                return instance;
            }
        }

        private static CategoriesService instance { get; set; }

        private CategoriesService()
        {

        }

        #endregion



        public List<Category> GetAllCategories()
        {
            
            using (var context = new CBContext())
            {

                return context.Categories.Include(x => x.Products).ToList();
                   
                
            }

        }


        #region CRUD & Pagination
        public List<Category> GetCategories(string search, int pageNo)
        {
            int pageSize = int.Parse(ConfigurationService.Instance.GetConfig("PageSize").Value); // get the page size from the config key

            using (var context = new CBContext())  
            {
                if (!string.IsNullOrEmpty(search)) // check if 'search' param holds a value
                {
                    return context.Categories
                        .Where(x => x.Name != null && x.Name.ToLower().Contains(search.ToLower())) 
                        .OrderBy(x => x.ID)
                        .Skip((pageNo - 1) * pageSize)
                        .Take(pageSize)
                        .Include(x => x.Products)
                        .ToList();
                }
                else // return all categories 
                {
                    return context.Categories
                        .OrderBy(x => x.ID)
                        .Skip((pageNo - 1) * pageSize)
                        .Take(pageSize)
                        .Include(x => x.Products)
                        .ToList();

                }


            }

        }

        public int GetCategoriesCount(string search)
        {
         
            using (var context = new CBContext())
            {

                if (!string.IsNullOrEmpty(search)) // check if 'search' param holds a value
                {
                    return context.Categories
                        .Where(x => x.Name != null && x.Name.ToLower().Contains(search.ToLower()))
                        .Count();
                       
    
                }
                else // return count of all categories
                {
                    return context.Categories.Count();

                }

               

            }

        }

        public void SaveCategory(Category category)
        {
            using (var context = new CBContext()) // create an object of 'CBContext' 
            {
                context.Categories.Add(category); // add 'category' argument to the database 'Categories', will be stored in memory not in database yet
                context.SaveChanges(); // will save it into the database
            }

        }




        public void UpdateCategory(Category category)
        {
            using (var context = new CBContext())
            {
                context.Entry(category).State = System.Data.Entity.EntityState.Modified; // updates the categories database
                context.SaveChanges();

            }

        }


        public void DeleteCategory(int ID)
        {
            using (var context = new CBContext())
            {
                var categoryToRemove = context.Categories.Where(x => x.ID == ID).Include(x => x.Products).FirstOrDefault();

                context.Products.RemoveRange(categoryToRemove.Products); // remove products of the category before removing the category
                context.Categories.Remove(categoryToRemove); // remove that category
                context.SaveChanges();

            }

        }
        #endregion

        public List<Category> GetFeaturedCategories()
        {
            using (var context = new CBContext())
            {
                // return the categories from the database where 'isFeatured' is equal to true and 'ImageURL' is not null, as a list
                return context.Categories.Where(x=>x.isFeatured && x.ImageURL != null).ToList(); 
            }

        }

        public  Category GetCategory(int ID)
        {
            using (var context = new CBContext())
            {
                return context.Categories.Find(ID); // return the category from the categories database based on the ID

            }

        }


    }
}
