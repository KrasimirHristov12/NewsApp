using Microsoft.AspNetCore.Mvc;
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

        public async Task<IViewComponentResult> InvokeAsync(int n)
        {
            var latestArticles = articlesService.GetLatest(5);
            return View(latestArticles);
        }
    }
}
