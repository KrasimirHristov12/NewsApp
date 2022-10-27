using System.ComponentModel.DataAnnotations;

namespace NewsApp.Models.Users
{
    public class LoginInputModel
    {
        [Required]
        [StringLength(20, MinimumLength = 6)]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        [DataType(DataType.Password)]
        
        public string Password { get; set; }
    }
}
