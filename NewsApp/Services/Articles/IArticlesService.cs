using Microsoft.AspNetCore.Mvc.ModelBinding;
using NewsApp.Models.Articles;

namespace NewsApp.Services.Articles
{
    public interface IArticlesService
    {
        IEnumerable<ArticlesViewModel> GetAll();
        Task<bool> AddAsync(ArticlesInputModel articleData, ModelStateDictionary modelState);
        IEnumerable<ArticlesViewModel> GetArticlesByCategory(string categoryName);
        Task<ArticlesViewModel> GetByIdAsync(string id);

        Task<bool> DeleteArticleByIdAsync(string id);
        Task UpdateAsync(ArticlesViewModel articles);


    }
}
