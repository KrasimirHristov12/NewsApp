using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NewsApp.Data;
using NewsApp.Data.Models;
using NewsApp.Models.Articles;
using NewsApp.Services.Categories;

namespace NewsApp.Services.Articles
{
    public class ArticlesService : IArticlesService
    {
        private readonly IRepository repo;

        public ArticlesService(IRepository articlesRepo)
        {
            this.repo = articlesRepo;
        }

        public async Task<bool> AddAsync(ArticlesViewModel articleData, ModelStateDictionary modelState)
        {

            if (await repo.GetByIdAsync<Category>(articleData.Category) == null)
            {
                modelState.AddModelError("Category", "The category does not exist!");
            }
            if (!modelState.IsValid)
            {
                return false;
            }

            var article = new Article()
            {
                Title = articleData.Title,
                Content = articleData.Content,
                CategoryId = Guid.Parse(articleData.Category),
            };
            await repo.AddAsync<Article>(article);
            return true;
        }

        public IEnumerable<ArticlesViewModel> GetArticlesByCategory(string categoryName)
        {
            return repo.GetAll<Article>()
                .Where(a => a.Category.Name == categoryName)
                .Select(a => new ArticlesViewModel
                {
                    Title = a.Title,
                    Content = a.Content,
                    Category = a.CategoryId.ToString(),
                })
                .ToList();

        }
    }
}
