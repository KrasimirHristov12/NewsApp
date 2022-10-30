using Microsoft.AspNetCore.Mvc;
using NewsApp.Services.Comments;

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
            var comments = commentsService.GetAllForArticle(Guid.Parse(articleId));
            return View(comments);

        }
    }
}
