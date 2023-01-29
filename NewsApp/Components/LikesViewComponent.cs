using Microsoft.AspNetCore.Mvc;
using NewsApp.Data;
using NewsApp.Models.Likes;
using NewsApp.Services.Likes;
using System.Security.Claims;

namespace NewsApp.Components
{
    public class LikesViewComponent : ViewComponent
    {
        private readonly ILikesService likesService;

        public LikesViewComponent(ILikesService likesService)
        {
            this.likesService = likesService;
        }
        public  IViewComponentResult Invoke(string articleId)
        {
            var userId = User.Identity.IsAuthenticated ? (User as ClaimsPrincipal).FindFirstValue(ClaimTypes.NameIdentifier) : null;
            var likesModel = likesService.GetAllLikesForArticle(articleId, userId);
            return View(likesModel);
        }
    }
}
