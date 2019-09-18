using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameRPG.BL.Models
{
   
    public class Shop
    {
       
        public string Name { get; set; }
        
        public List<Item> Items { get; set; }

        
 
        public Shop(string name, List<Item> items)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("", nameof(name));
            }
            Name = name;
            Items = items ?? throw new ArgumentNullException("", nameof(items));
        }
    }
}
