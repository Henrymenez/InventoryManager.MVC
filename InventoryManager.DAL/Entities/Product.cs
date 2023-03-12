using InventoryManager.DAL.Enums;

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
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
