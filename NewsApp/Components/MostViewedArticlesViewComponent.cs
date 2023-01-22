using Microsoft.AspNetCore.Mvc;
using NewsApp.Models.Articles;
using NewsApp.Services.Articles;

namespace NewsApp.Components
{
    public class MostViewedArticlesViewComponent : ViewComponent
    {
        private readonly IArticlesService articlesService;

        public MostViewedArticlesViewComponent(IArticlesService articlesService)
        {
            this.articlesService = articlesService;
        }

        public IViewComponentResult Invoke(int n)
        {
            var mostWatchedArticles = articlesService.GetMostWatched<HomeArticlesViewModel>(n);
            return View(mostWatchedArticles);
        }
    }
}
