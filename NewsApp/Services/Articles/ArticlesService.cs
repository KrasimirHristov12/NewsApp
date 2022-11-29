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

        public IEnumerable<ArticlesViewModel> GetAll()
        {
            return repo.GetAll<Article>()
                .Select(a => new ArticlesViewModel
                {
                    Title = a.Title,
                    Category = a.CategoryId.ToString(),
                    Content = a.Content,
                    Id = a.Id.ToString(),
                })
                .ToList();
        }

        public IEnumerable<ArticlesViewModel> GetPerPage(int numberPerPage, int currentPage)
        {
           return repo.GetAll<Article>()
                .Select(a => new ArticlesViewModel
                {
                    Title = a.Title,
                    Category = a.CategoryId.ToString(),
                    Content = a.Content,
                    Id = a.Id.ToString(),
                })
                .Skip((currentPage - 1) * numberPerPage)
                .Take(numberPerPage)
                .ToList();
        }

        public async Task<bool> AddAsync(ArticlesInputModel articleData, ModelStateDictionary modelState, string userId)
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
                UserId = userId
            };
            await repo.AddAsync<Article>(article);
            return true;
        }

        public async Task<bool> DeleteArticleByIdAsync(string id, string userId)
        {
            var article = await repo.GetByIdAsync<Article>(id);
            
            if (article == null)
            {
                return false;
            }
            if (article.UserId != userId)
            {
                return false;
            }
            await repo.DeleteAsync<Article>(Guid.Parse(id));
            return true;
        }

        public async Task<ArticlesViewModel> GetByIdAsync(string id)
        {
            var article = await repo.GetByIdAsync<Article>(id);
            if (article == null)
            {
                return null;
            }
            return new ArticlesViewModel()
            {
                Id = article.Id.ToString(),
                Title = article.Title,
                Content = article.Content,
                Category = article.CategoryId.ToString(),
            };
        }

        public IEnumerable<ArticlesViewModel> GetArticlesByCategory(string categoryName)
        {
            return repo.GetAll<Article>()
                .Where(a => a.Category.Name == categoryName)
                .Select(a => new ArticlesViewModel
                {
                    Id = a.Id.ToString(),
                    Title = a.Title,
                    Content = a.Content,
                })
                .ToList();

        }
        public async Task UpdateAsync(ArticlesViewModel articles)
        {
            var article = await repo.GetByIdAsync<Article>(articles.Id);
            article.Title = articles.Title;
            article.CategoryId = Guid.Parse(articles.Category);
            article.Content = articles.Content;
            await repo.UpdateAsync(article);
        }

        public async Task<ArticlesViewModel> GetYoursByIdAsync(string id, string userId)
        {
            var article = await repo.GetByIdAsync<Article>(id);
            if (article == null)
            {
                return null;
            }
            if (article.UserId != userId)
            {
                return null;
            }

            return new ArticlesViewModel()
            {
                Id = article.Id.ToString(),
                Title = article.Title,
                Content = article.Content,
                Category = article.CategoryId.ToString(),
            };



        }

        public IEnumerable<HomeArticlesViewModel> GetLatest(int n)
        {
            return repo.GetAll<Article>().OrderByDescending(a => a.CreatedOn).Take(n)
                .Select(a => new HomeArticlesViewModel
                {
                    Id = a.Id.ToString(),
                    Title = a.Title
                })
                .ToList();
        }
    }
}
