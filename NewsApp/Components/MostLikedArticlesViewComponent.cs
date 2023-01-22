using Microsoft.AspNetCore.Mvc;
using NewsApp.Models.Articles;
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
        public  IViewComponentResult Invoke(int n)
        {
            var mostLikedArticles = articlesService.GetMostLiked<HomeArticlesViewModel>(n);
            return View(mostLikedArticles);
        }
    }
}
