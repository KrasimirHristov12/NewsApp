using Microsoft.AspNetCore.Mvc;
using NewsApp.Common;
using NewsApp.Models.Articles;
using NewsApp.Services.Articles;
using System.Security.Claims;

namespace NewsApp.Areas.Administration.Controllers
{
    [Area(WebConstants.Area.AdministrationName)]
    public class ArticlesController : BaseAdminController
    {
        private readonly IArticlesService articlesService;

        public ArticlesController(IArticlesService articlesService)
        {
            this.articlesService = articlesService;
        }

        public IActionResult All(int page)
        {
            var allArticles = articlesService.GetPerPage<DisplayArticlesToAdminViewModel>(20, page);
            return View(allArticles);
        }
        public async Task<IActionResult> Delete(string articleId)
        {
            var adminId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var success = await articlesService.DeleteArticleByIdAsync(articleId, adminId);
            return RedirectToAction(nameof(All), new {page = 1});

        }


    }
}
