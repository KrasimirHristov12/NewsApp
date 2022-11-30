using Microsoft.AspNetCore.Mvc;
using NewsApp.Models.Articles;
using NewsApp.Services.Articles;
using NewsApp.Services.Categories;
using NewsApp.Models.Categories;
using NewsApp.Data.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace NewsApp.Controllers
{
    public class ArticlesController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly IArticlesService articlesService;

        public ArticlesController(ICategoriesService categoriesService, IArticlesService articlesService)
        {
            this.categoriesService = categoriesService;
            this.articlesService = articlesService;
        }
        [AllowAnonymous]
        public IActionResult All(int page)
        {
            var totalArticlesCount = articlesService.GetAll().Count();
            ViewData["ArticlesCount"] = totalArticlesCount;
            var articles = articlesService.GetPerPage(6, page);
            return View(articles);
        }
        public IActionResult Add()
        {
            var articlesModel = new ArticlesInputModel()
            {
                Categories = categoriesService.GetAll()
            };
            return View(articlesModel);
        }
        [HttpPost]
        public async Task<IActionResult> Add(ArticlesInputModel article)
        {

            article.Categories = categoriesService.GetAll();

            

            if (!ModelState.IsValid)
            {
                
                return View(article);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool addResult = await articlesService.AddAsync(article, ModelState, userId);
            if (!addResult)
            {
                
                return View(article);
            }

            return RedirectToAction(nameof(All), new {page = 1});

        }

        public async Task<IActionResult> Delete(string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool deleteResult = await articlesService.DeleteArticleByIdAsync(id, userId);
            if (!deleteResult)
            {
                return NotFound(); // Not allowed page if user is different from the article's author, not found if invalid article
            }
            return Ok("This article was successfully deleted");
            
        }

        [AllowAnonymous]
        public IActionResult ByCategory(string name)
        {

            var articles = articlesService.GetArticlesByCategory(name);
            ViewData["name"] = name;
            return View(articles);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            var article = await articlesService.GetByIdAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            int views = await articlesService.IncrementViewsAsync(id);
            ViewData["Views"] = views;
            return View(article);
        }
        public async Task<IActionResult> Update(string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var article =  await articlesService.GetYoursByIdAsync(id, userId);  
            if (article == null)
            {
                return NotFound();
            }
            article.Categories = categoriesService.GetAll();
            return View(article);


        }
        [HttpPost]
        public async Task<IActionResult> Update(ArticlesViewModel articles)
        {
            articles.Categories = categoriesService.GetAll();

            if (!ModelState.IsValid)
            {
                return View(articles);
            }
            // Check in case the hidden field for id refers to non-existing article
            var articleModel = await articlesService.GetByIdAsync(articles.Id);

            if (articleModel == null)
            {
                return NotFound();
            }
            await articlesService.UpdateAsync(articles);
            return RedirectToAction(nameof(Details),new { id = articles.Id});


          


        }
    }
}
