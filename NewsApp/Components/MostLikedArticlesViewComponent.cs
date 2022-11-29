using Microsoft.AspNetCore.Mvc;
using NewsApp.Services.Articles;

namespace NewsApp.Components
{
    public class MostLikedArticlesViewComponent : ViewComponent
    {
        private readonly IArticlesService articlesService;

        public MostLikedArticlesViewComponent(IArticlesService articlesService)
        {
            this.articlesService = articlesService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int n)
        {
            var mostLikedArticles = articlesService.GetMostLiked(n);
            return View(mostLikedArticles);
        }
    }
}
