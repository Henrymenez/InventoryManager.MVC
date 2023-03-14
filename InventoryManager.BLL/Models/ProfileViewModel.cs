using InventoryManager.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace InventoryManager.BLL.Models
{
    public class ProfileViewModel
    {

        [Required, StringLength(50, ErrorMessage = "Fullname character limit is between 3 and 50", MinimumLength = 10)]
        public string FullName { get; set; }

        [Required, StringLength(50, ErrorMessage = "Email character limit is between 3 and 50", MinimumLength = 10)]
        public string Email { get; set; }

        [StringLength(15, ErrorMessage = "Phone charater limit is between 11 and 15", MinimumLength = 11)]
        public string Phone { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

    }
}
