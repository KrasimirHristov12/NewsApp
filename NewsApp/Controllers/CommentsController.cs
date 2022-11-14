using Microsoft.AspNetCore.Mvc;
using NewsApp.Models.Comments;
using NewsApp.Services.Comments;

namespace NewsApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsService commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            this.commentsService = commentsService;
        }
        [HttpPost]
        public IActionResult Post([FromBody]CommentsInputModel commentModel)
        {
            var comment = commentsService.Add(commentModel);
            return Ok(comment);
        }
    }
}
