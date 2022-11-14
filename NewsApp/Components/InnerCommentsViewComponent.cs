using Microsoft.AspNetCore.Mvc;
using NewsApp.Data.Models;
using NewsApp.Models.Comments;
using NewsApp.Services.Comments;
using System.Security.Claims;

namespace NewsApp.Components
{
    public class InnerCommentsViewComponent : ViewComponent
    {
        private readonly ICommentsService commentsService;

        public InnerCommentsViewComponent(ICommentsService commentsService)
        {
            this.commentsService = commentsService;
        }
        public async Task<IViewComponentResult> InvokeAsync(string commentId, string articleId)
        {
            var comments = new CommentsViewModel()
            {
                ArticleId = articleId,
                UserId = UserClaimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier),
                Comments = commentsService.GetInnerComments(Guid.Parse(commentId))
            };
            return View(comments);
        }
    }
}
