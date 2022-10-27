using System.ComponentModel.DataAnnotations;

namespace NewsApp.Models.Users
{
    public class RegisterInputModel
    {
        [Required]
        [StringLength(20, MinimumLength = 6)]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [StringLength(60,MinimumLength = 10)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(20,MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6)]
        [Compare(nameof(Password))]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
