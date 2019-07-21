using Ecommerce.Database;
using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services
{
    public static class ConfigurationService
    {
        public static Config GetConfig(string key)
        {
            using (var context = new CBContext())
            {
                return context.Configurations.Find(key);

            }
        } 
    }
}
