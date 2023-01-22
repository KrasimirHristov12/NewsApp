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

            var likesModel = likesService.GetAllLikesForArticle(articleId);
            return View(likesModel);
        }
    }
}
