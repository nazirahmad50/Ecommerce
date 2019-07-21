using Ecommerce.Database;
using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services
{
    public class ConfigurationService
    {
        // create a signleton pattern
        public static ConfigurationService ClassObject
        {
            get
            {
                // 'PrivateInMemoryObject' doesnt exist in memory then create 'PrivateInMemoryObject' obejct
                if (PrivateInMemoryObject == null) PrivateInMemoryObject = new ConfigurationService();

                // if its not null just return 'PrivateInMemoryObject'
                return PrivateInMemoryObject;
            }
        }

        private static ConfigurationService PrivateInMemoryObject { get; set; }

        private ConfigurationService()
        {

        }


        public Config GetConfig(string key)
        {
            using (var context = new CBContext())
            {
                return context.Configurations.Find(key);

            }
        } 
    }
}
