using Microsoft.AspNetCore.Mvc;
using NewsApp.Models.Articles;
using NewsApp.Services.Articles;

namespace NewsApp.Components
{
    public class LatestArticlesViewComponent : ViewComponent
    {
        private readonly IArticlesService articlesService;

        public LatestArticlesViewComponent(IArticlesService articlesService)
        {
            this.articlesService = articlesService;
        }

        public IViewComponentResult Invoke(int n)
        {
            var latestArticles = articlesService.GetLatest<HomeArticlesViewModel>(n);
            return View(latestArticles);
        }
    }
}
