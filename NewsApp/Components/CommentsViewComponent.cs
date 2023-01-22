using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsApp.Data.Models;
using NewsApp.Models.Comments;
using NewsApp.Services.Comments;
using System.Security.Claims;

namespace NewsApp.Components
{
    public class CommentsViewComponent : ViewComponent
    {
        private readonly ICommentsService commentsService;

        public CommentsViewComponent(ICommentsService commentsService)
        {
            this.commentsService = commentsService;
        }
        public IViewComponentResult Invoke(string articleId)
        {
            var comments = new CommentsViewModel()
            {
                ArticleId = articleId,
                UserId = UserClaimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier),
                Comments = commentsService.GetAllForArticle<DisplayCommentsViewModel>(Guid.Parse(articleId))
            };
            return View(comments);

        }
    }
}
