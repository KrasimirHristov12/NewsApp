using System.ComponentModel.DataAnnotations;

namespace NewsApp.Models.Authors
{
    public class AuthorsInputModel
    {
        [Display(Name = "First Name")]
        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string MiddleName { get; set; }
        [Display(Name = "Last Name")]
        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Display(Name = "Years of experience")]
        [Range(1, 50)]

        public int YearsOfExperience { get; set; }

        [Display(Name = "Cover Letter")]
        [Required]
        [MinLength(200)]
        [MaxLength(10000)]
        [DataType(DataType.MultilineText)]
        public string CoverLetter { get; set; }


    }
}
