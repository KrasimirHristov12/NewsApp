using Microsoft.AspNetCore.Mvc;
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

        public async Task<IViewComponentResult> InvokeAsync(int n)
        {
            var mostWatchedArticles = articlesService.GetMostWatched(n);
            return View(mostWatchedArticles);
        }
    }
}
