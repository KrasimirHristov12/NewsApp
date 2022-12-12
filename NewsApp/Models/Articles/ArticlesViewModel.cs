using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NewsApp.Models.Categories;
using System.ComponentModel.DataAnnotations;

namespace NewsApp.Models.Articles
{
    
    public class ArticlesViewModel
    { 
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }

        public string ImageName { get; set; }
        public string UserId { get; set; }

        public IEnumerable<CategoriesViewModel>? Categories { get; set; }

    }
}
