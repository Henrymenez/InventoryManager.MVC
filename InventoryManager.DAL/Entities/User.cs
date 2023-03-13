using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.DAL.Entities
{
    public class User : BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; } 
        public string Password { get; set; }
        public IList<Product> Products { get; set; } = new List<Product>();
    }
}
