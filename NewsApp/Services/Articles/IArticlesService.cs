using Microsoft.AspNetCore.Mvc.ModelBinding;
using NewsApp.Models.Articles;

namespace NewsApp.Services.Articles
{
    public interface IArticlesService
    {
        int GetArticlesCount();
        Task<bool> AddAsync(ArticlesInputModel articleData, ModelStateDictionary modelState, string userId);
        IEnumerable<T> GetArticlesByCategory<T>(string categoryName);

        Task<T> GetByIdAsync<T>(string id);


        Task<bool> ExistsById(string id);

        Task<T?> GetYoursByIdAsync<T>(string id, string userId);
        Task<bool?> DeleteArticleByIdAsync(string id, string userId);
        Task UpdateAsync(ArticlesInputModel articleInputModel, string articleId);

        IEnumerable<T> GetPerPage<T>(int numberPerPage, int currentPage);

        IEnumerable<T> GetLatest<T>(int n);

        IEnumerable<T> GetMostWatched<T>(int n);
        IEnumerable<T> GetMostLiked<T>(int n);

        Task<int> IncrementViewsAsync(string articleId);




    }
}
