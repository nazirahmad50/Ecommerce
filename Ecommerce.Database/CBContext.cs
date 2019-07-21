using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Database
{
    public class CBContext : DbContext
    {
        public CBContext() : base ("EcommerceConnection") // pass the connection string that we want to create on to the Entity constructor which is in base class 'DbContext'
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Config> Configurations { get; set; }

    }
}
