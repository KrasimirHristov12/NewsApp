using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NewsApp.Data;
using NewsApp.Data.Models;
using NewsApp.Models.Articles;
using NewsApp.Services.Categories;
using NewsApp.Services.Files;
using NewsApp.Services.Mapping;

namespace NewsApp.Services.Articles
{
    public class ArticlesService : IArticlesService
    {
        private readonly IRepository repo;
        private readonly IFilesService filesService;
        private readonly IWebHostEnvironment webHost;
        private readonly IMapper mapper;

        public ArticlesService(IRepository articlesRepo,
            IFilesService filesService,
            IWebHostEnvironment webHost,
            IMapper mapper)
        {
            this.repo = articlesRepo;
            this.filesService = filesService;
            this.webHost = webHost;
            this.mapper = mapper;
        }


        public int GetArticlesCount()
        {
            return repo.GetAll<Article>().Count();
        }

        public IEnumerable<T> GetPerPage<T>(int numberPerPage, int currentPage)
        {
            return repo.GetAll<Article>()
                 .To<T>()
                 .Skip((currentPage - 1) * numberPerPage)
                 .Take(numberPerPage)
                 .ToList();
        }

        public async Task<bool> AddAsync(ArticlesInputModel articleData, ModelStateDictionary modelState, string userId)
        {
            if (!modelState.IsValid)
            {
                return false;
            }

            var article = new Article()
            {
                Title = articleData.Title,
                Content = articleData.Content,
                ImageName = articleData.Image != null ? articleData.Image.FileName : null,
                CategoryId = Guid.Parse(articleData.Category),
                UserId = userId
            };
            await repo.AddAsync(article);

            if (article.ImageName != null)
            {
                string path = Path.Combine(webHost.WebRootPath, "images", "articles");
                await filesService.UploadAsync(path, articleData.Image);

            }
            return true;

        }

        public async Task<bool?> DeleteArticleByIdAsync(string id, string userId)
        {
            var article = await repo.GetByIdAsync<Article>(id);

            if (article == null)
            {
                return null;
            }
            if (article.UserId != userId)
            {
                return false;
            }
            await repo.DeleteAsync<Article>(Guid.Parse(id));
            return true;
        }

        public async Task<bool> ExistsById(string id)
        {
            var article = await repo.GetByIdAsync<Article>(id);
            return article != null;
        }

        public async Task<T> GetByIdAsync<T>(string id)
        {
            var article = await repo.GetByIdAsync<Article>(id);
            return mapper.Map<T>(article);

        }

        public IEnumerable<T> GetArticlesByCategory<T>(string categoryName)
        {
            return repo.GetAll<Article>()
                .Where(a => a.Category.Name == categoryName)
                .To<T>()
                .ToList();

        }
        public async Task UpdateAsync(ArticlesInputModel articleInputModel, string articleId)
        {
            var article = await repo.GetByIdAsync<Article>(articleId);
            article.Title = articleInputModel.Title;
            article.CategoryId = Guid.Parse(articleInputModel.Category);
            article.Content = articleInputModel.Content;
            article.ImageName = articleInputModel.Image != null ? articleInputModel.Image.FileName : null;
            if (article.ImageName != null)
            {
                string path = Path.Combine(webHost.WebRootPath, "images", "articles");
                await filesService.UploadAsync(path, articleInputModel.Image);

            }
            await repo.UpdateAsync(article);
        }

        public async Task<T?> GetYoursByIdAsync<T>(string id, string userId)
        {
            var article = await repo.GetByIdAsync<Article>(id);
            if (article == null)
            {
                return default(T);
            }
            if (article.UserId != userId)
            {
                return default(T);
            }

            return mapper.Map<T>(article);

        }




        public IEnumerable<T> GetLatest<T>(int n)
        {
            return repo.GetAll<Article>().OrderByDescending(a => a.CreatedOn).Take(n)
                .To<T>()
                .ToList();
        }

        public async Task<int> IncrementViewsAsync(string articleId)
        {
            ArticleViews? views = repo.GetAll<ArticleViews>().FirstOrDefault(v => v.ArticleId == Guid.Parse(articleId));
            if (views == null)
            {
                views = new ArticleViews
                {
                    ArticleId = Guid.Parse(articleId),
                    ViewsCount = 1,
                };
                await repo.AddAsync(views);
            }
            else
            {
                views.ViewsCount++;
                await repo.UpdateAsync(views);
            }

            return views.ViewsCount;

        }

        public IEnumerable<T> GetMostWatched<T>(int n)
        {
            return repo.GetAll<Article>().OrderByDescending(a => a.ArticleViews.First().ViewsCount)
                 .Take(n)
                 .To<T>()
                 .ToList();
        }

        public IEnumerable<T> GetMostLiked<T>(int n)
        {
            return repo.GetAll<Article>().OrderByDescending(a => a.UserArticleLikes.Count)
                .Take(n)
                .To<T>()
                .ToList();
        }
    }
}
