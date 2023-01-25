using Microsoft.AspNetCore.Mvc;
using NewsApp.Models.Articles;
using NewsApp.Services.Articles;

namespace NewsApp.Components
{
    public class UpdateArticleFormViewComponent : ViewComponent
    {
        private readonly IArticlesService articlesService;

        public UpdateArticleFormViewComponent(IArticlesService articlesService)
        {
            this.articlesService = articlesService;
        }
        public async Task<IViewComponentResult> InvokeAsync(string articleId, string userId)
        {
            var article = await articlesService.GetArticleIfYours<UpdateArticleInputModel>(articleId, userId);
            return View(article);
        }
    }
}
