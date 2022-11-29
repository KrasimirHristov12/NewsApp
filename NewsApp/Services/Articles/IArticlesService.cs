using Microsoft.AspNetCore.Mvc.ModelBinding;
using NewsApp.Models.Articles;

namespace NewsApp.Services.Articles
{
    public interface IArticlesService
    {
        IEnumerable<ArticlesViewModel> GetAll();
        Task<bool> AddAsync(ArticlesInputModel articleData, ModelStateDictionary modelState, string userId);
        IEnumerable<ArticlesViewModel> GetArticlesByCategory(string categoryName);
        Task<ArticlesViewModel> GetByIdAsync(string id);

        Task<ArticlesViewModel> GetYoursByIdAsync(string id, string userId);
        Task<bool> DeleteArticleByIdAsync(string id, string userId);
        Task UpdateAsync(ArticlesViewModel articles);

        IEnumerable<ArticlesViewModel> GetPerPage(int numberPerPage, int currentPage);

        IEnumerable<HomeArticlesViewModel> GetLatest(int n);

        IEnumerable<HomeArticlesViewModel> GetMostWatched(int n);
        IEnumerable<HomeArticlesViewModel> GetMostLiked(int n);

        Task<int> IncrementViewsAsync(string articleId);



    }
}
