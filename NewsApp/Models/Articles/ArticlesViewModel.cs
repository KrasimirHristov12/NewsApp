using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NewsApp.Models.Categories;
using System.ComponentModel.DataAnnotations;

namespace NewsApp.Models.Articles
{
    
    public class ArticlesViewModel
    {
        [Required]
        public string Id { get; set; }

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

        public string ImageName { get; set; }
        public string UserId { get; set; }

        public IEnumerable<CategoriesViewModel>? Categories { get; set; }

    }
}
