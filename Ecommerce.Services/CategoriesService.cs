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
    public class CategoriesService
    {
        public void SaveCategory(Category category)
        {
            using (var context = new CBContext()) // create an object of 'CBContext' 
            {
                context.Categories.Add(category); // add 'category' argument to the database 'Categories', will be stored in memory not in database yet
                context.SaveChanges(); // will save it into the database
            }

        }

        public List<Category> GetCategories()
        {
            using (var context = new CBContext())  
            {
                return context.Categories.Include(x => x.Products).ToList(); // return the categories from the database as a list

            }

        }

        public List<Category> GetFeaturedCategories()
        {
            using (var context = new CBContext())
            {
                // return the categories from the database where 'isFeatured' is equal to true and 'ImageURL' is not null, as a list
                return context.Categories.Where(x=>x.isFeatured && x.ImageURL != null).ToList(); 
            }

        }

        public Category GetCategory(int ID)
        {
            using (var context = new CBContext())
            {
                return context.Categories.Find(ID); // return the category from the categories database based on the ID

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
                var categoryToRemove = context.Categories.Find(ID); // find the category to remove based on the ID parameter

                context.Categories.Remove(categoryToRemove); // remove that category
                context.SaveChanges();

            }

        }
    }
}
