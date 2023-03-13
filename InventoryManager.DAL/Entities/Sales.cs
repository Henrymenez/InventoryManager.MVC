using InventoryManager.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.DAL.Entities
{
    public class Sales : BaseEntity
    {
        public string Name { get; set; }
        public Category Category { get; set; }
        public long Quantity { get; set; }
        public decimal Price { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ProductId { get; set; }
        public IList<Product> Products { get; set;}
    }
}
