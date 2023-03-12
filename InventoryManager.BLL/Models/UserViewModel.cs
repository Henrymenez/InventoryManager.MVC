using System.ComponentModel.DataAnnotations;

namespace InventoryManager.BLL.models
{
    public class UserViewModel
    {

        [Required, StringLength(50, ErrorMessage = "Fullname character limit is between 3 and 50", MinimumLength = 10)]
        public string FullName { get; set; }

        [Required, StringLength(50, ErrorMessage = "Email character limit is between 3 and 50", MinimumLength = 10)]
        public string Email { get; set; }

        [StringLength(15, ErrorMessage = "Phone charater limit is between 11 and 15", MinimumLength = 11)]
        public string Phone { get; set; }
        [Required, StringLength(38, ErrorMessage = "Password charater limit is between 8 and 38", MinimumLength = 8)]
        public string Password { get; set; }
        [Required, StringLength(38, ErrorMessage = "Confirm Password charater limit is between 8 and 38", MinimumLength = 8)]
        public string ConfirmPassword { get; set; }



    }
}
