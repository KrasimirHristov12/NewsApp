using NewsApp.Common;
using NewsApp.Data.Models;
using NewsApp.Models.Categories;
using NewsApp.Services.Mapping;
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

        [Display(Name = WebConstants.Article.ContentDisplay)]
        [DataType(DataType.MultilineText)]
        [Required]
        [MinLength(DataConstants.Article.ContentMinLength)]
        public string Content { get; set; }

        [Display(Name = WebConstants.Article.CategoryDisplay)]
        [Required]
        [DoesCategoryExistInDb]
        public string CategoryId { get; set; }

        public IEnumerable<CategoriesViewModel>? Categories { get; set; }
    }
}
