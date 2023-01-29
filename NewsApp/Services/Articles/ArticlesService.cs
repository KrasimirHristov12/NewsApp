using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NewsApp.Common;
using NewsApp.Data;
using NewsApp.Data.Models;
using NewsApp.Models.Articles;
using NewsApp.Services.Categories;
using NewsApp.Services.Files;
using NewsApp.Services.Mapping;
using System.Diagnostics.Eventing.Reader;

namespace NewsApp.Services.Articles
{
    public class ArticlesService : IArticlesService
    {
        private readonly IRepository repo;
        private readonly IFilesService filesService;
        private readonly IWebHostEnvironment webHost;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;

        public ArticlesService(IRepository articlesRepo,
            IFilesService filesService,
            IWebHostEnvironment webHost,
            IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            this.repo = articlesRepo;
            this.filesService = filesService;
            this.webHost = webHost;
            this.mapper = mapper;
            this.userManager = userManager;
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

        public async Task<bool> AddAsync(AddArticlesInputModel articleData, ModelStateDictionary modelState, string userId)
        {
            string path = Path.Combine(webHost.WebRootPath, "images", "articles");
            if (!modelState.IsValid)
            {
                return false;
            }

            var article = new Article()
            {
                Title = articleData.Title,
                Content = articleData.Content,
                CategoryId = Guid.Parse(articleData.CategoryId),
                UserId = userId
            };
            foreach (var image in articleData.Images)
            {
                article.Images.Add(new Image
                {
                    Name = image.FileName,
                    Extension = Path.GetExtension(image.FileName),
            });
            }

           
            await filesService.UploadAsync(path, articleData.Images);

            await repo.AddAsync(article);
            return true;

        }

        public async Task<bool> DeleteArticleByIdAsync(string id, string userId)
        {
            var article = await repo.GetByIdAsync<Article>(id);
            var inAdminRole = await IsCurrentUserAdmin(userId);

            if (article == null)
            {
                return false;
            }
            if (article.UserId != userId && !inAdminRole)
            {
                return false;
            }
            await repo.DeleteAsync<Article>(Guid.Parse(id));
            return true;
        }

        public async Task<bool> ExistsAndIfYoursById(string id, string userId)
        {
            var article = await repo.GetByIdAsync<Article>(id);
            bool inRoleAdmin = await IsCurrentUserAdmin(userId);
            if (article != null)
            {
                return article.UserId == userId || inRoleAdmin;

            }
            return false;

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
        public async Task UpdateAsync(UpdateArticleInputModel articleInputModel, string articleId)
        {
            var article = await repo.GetByIdAsync<Article>(articleId);
            article.Title = articleInputModel.Title;
            article.CategoryId = Guid.Parse(articleInputModel.CategoryId);
            article.Content = articleInputModel.Content;
            await repo.UpdateAsync(article);
        }

        public async Task<T?> GetArticleIfYours<T>(string id, string userId)
        {
            var article = await repo.GetByIdAsync<Article>(id);
            var isUserAdmin = await IsCurrentUserAdmin(userId);
            if (article == null)
            {
                return default(T);
            }
            if (article.UserId != userId && !isUserAdmin)
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

        private async Task<bool> IsCurrentUserAdmin(string userId)
        {
            var currentUser = await this.userManager.FindByIdAsync(userId);
            bool inAdminRole = await userManager.IsInRoleAsync(currentUser, WebConstants.Role.AdminRoleName);
            return inAdminRole;
        }
    }
}
