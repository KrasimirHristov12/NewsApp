using NewsApp.Models.Categories;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NewsApp.Models.Articles
{
    public class ArticlesInputModel
    {
        [Display(Name = "Article Title:")]
        [Required]
        [StringLength(300, MinimumLength = 10)]
        public string Title { get; set; }
        [Display(Name = "Article Content:")]
        [DataType(DataType.MultilineText)]
        [Required]
        [MinLength(30)]
        public string Content { get; set; }
        [Display(Name = "Article Category:")]
        [Required]
        public string Category { get; set; }



        public IEnumerable<CategoriesViewModel>? Categories { get; set; }
    }
}
