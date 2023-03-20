using InventoryManager.DAL.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManager.DAL.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public Category Category { get; set; }
        public long Quantity { get; set; }
        public decimal Price { get; set; }
        public string  BrandName { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        public IList<Sales> Sales { get; set; } = new List<Sales>();
    }
}
