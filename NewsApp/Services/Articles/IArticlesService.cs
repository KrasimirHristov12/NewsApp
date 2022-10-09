using Microsoft.AspNetCore.Mvc.ModelBinding;
using NewsApp.Models.Articles;

namespace NewsApp.Services.Articles
{
    public interface IArticlesService
    {
        Task<bool> AddAsync(ArticlesViewModel articleData, ModelStateDictionary modelState);
        IEnumerable<ArticlesViewModel> GetArticlesByCategory(string categoryName);
    }
}
