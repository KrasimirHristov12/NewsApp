using Microsoft.AspNetCore.Mvc;
using NewsApp.Models.Comments;
using NewsApp.Services.Comments;

namespace NewsApp.Components
{
    public class InnerCommentsViewComponent : ViewComponent
    {
        private readonly ICommentsService commentsService;

        public InnerCommentsViewComponent(ICommentsService commentsService)
        {
            this.commentsService = commentsService;
        }
        public IViewComponentResult Invoke(string outerCommentId, int margin)
        {
            var innerComments = commentsService.GetInnerComments<DisplayCommentsViewModel>(Guid.Parse(outerCommentId));
            ViewData["margin"] = margin;
            return View(innerComments);
        }
    }
    
}
