using Microsoft.AspNetCore.Mvc;
using NewsApp.Models.Articles;
using NewsApp.Services.Articles;
using NewsApp.Services.Categories;
using NewsApp.Models.Categories;

namespace NewsApp.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IArticlesService articlesService;

        public ArticlesController(ICategoriesService categoriesService, IArticlesService articlesService)
        {
            this.categoriesService = categoriesService;
            this.articlesService = articlesService;
        }
        public IActionResult Add()
        {
            var articlesModel = new ArticlesViewModel()
            {
                Categories = categoriesService.GetAll()
            };
            return View(articlesModel);
        }
        [HttpPost]
        public async Task<IActionResult> Add(ArticlesViewModel article)
        {

            article.Categories = categoriesService.GetAll();

            if (!ModelState.IsValid)
            {
                
                return View(article);
            }
            bool addResult = await articlesService.AddAsync(article, ModelState);
            if (!addResult)
            {
                
                return View(article);
            }
            return Redirect("/");

        }


        public IActionResult ByCategory(string name)
        {
            var articles = articlesService.GetArticlesByCategory(name);
            return View(articles);
        }
    }
}
