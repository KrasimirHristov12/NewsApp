using Microsoft.AspNetCore.Mvc.ModelBinding;
using NewsApp.Models.Articles;

namespace NewsApp.Services.Articles
{
    public interface IArticlesService
    {
        int GetArticlesCount();
        Task<bool> AddAsync(ArticlesInputModel articleData, ModelStateDictionary modelState, string userId);
        IEnumerable<ListArticlesByCategoryViewModel> GetArticlesByCategory(string categoryName);

        Task<DisplayArticleViewModel> GetByIdAsync(string id);

        Task<bool> ExistsById(string id);

        Task<ArticlesInputModel> GetYoursByIdAsync(string id, string userId);
        Task<bool?> DeleteArticleByIdAsync(string id, string userId);
        Task UpdateAsync(ArticlesInputModel articleInputModel, string articleId);

        IEnumerable<ArticlesPagingViewModel> GetPerPage(int numberPerPage, int currentPage);

        IEnumerable<HomeArticlesViewModel> GetLatest(int n);

        IEnumerable<HomeArticlesViewModel> GetMostWatched(int n);
        IEnumerable<HomeArticlesViewModel> GetMostLiked(int n);

        Task<int> IncrementViewsAsync(string articleId);



    }
}
