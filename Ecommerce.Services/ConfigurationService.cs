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
        #region Singleton
        public static ConfigurationService Instance
        {
            get
            {
                // 'PrivateInMemoryObject' doesnt exist in memory then create 'PrivateInMemoryObject' obejct
                if (instance == null) instance = new ConfigurationService();

                // if its not null just return 'PrivateInMemoryObject'
                return instance;
            }
        }

        private static ConfigurationService instance { get; set; }

        private ConfigurationService()
        {

        }
        #endregion

        public Config GetConfig(string key)
        {
            using (var context = new CBContext())
            {
                return context.Configurations.Find(key);

            }
        }


        public List<Config> GetConfigs()
        {
            using (var context = new CBContext())
            {
                return context.Configurations.ToList();

            }
        }

        public void UpdateConfigs(Config config)
        {
            using (var context = new CBContext())
            {
                context.Entry(config).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

            }
        }
    }
}
