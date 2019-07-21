using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entities
{
    public class Config
    {
        [Key] // make they 'key' property as a primary key
        public string Key { get; set; } 

        public string Value { get; set; }
    }
}
