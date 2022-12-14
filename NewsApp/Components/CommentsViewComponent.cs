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
        public async Task<IViewComponentResult> InvokeAsync(string articleId)
        {
            var comments = new CommentsViewModel()
            {
                ArticleId = articleId,
                UserId = UserClaimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier),
                Comments = commentsService.GetAllForArticle(Guid.Parse(articleId))
            };
            return View(comments);

        }
    }
}
