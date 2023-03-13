using InventoryManager.DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace InventoryManager.BLL.Models
{
    public class SalesViewModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "UserId is required")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Product Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public Category Category { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public long Quantity { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Product is required")]
        public int ProductId { get; set; }

    }
}
