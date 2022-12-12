using NewsApp.Common;
using NewsApp.Models.Categories;
using NewsApp.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NewsApp.Models.Articles
{
    public class ArticlesInputModel
    {
        [Display(Name = WebConstants.Article.TitleDisplay)]
        [Required]
        [StringLength(DataConstants.Article.TitleMaxLength, MinimumLength = DataConstants.Article.TitleMinLength)]
        public string Title { get; set; }

        [ValidateFileExtension]
        public IFormFile? Image { get; set; }

        [Display(Name = WebConstants.Article.ContentDisplay)]
        [DataType(DataType.MultilineText)]
        [Required]
        [MinLength(DataConstants.Article.ContentMinLength)]
        public string Content { get; set; }
        [Display(Name = WebConstants.Article.CategoryDisplay)]
        [Required]
        public string Category { get; set; }





        public IEnumerable<CategoriesViewModel>? Categories { get; set; }
    }
}
