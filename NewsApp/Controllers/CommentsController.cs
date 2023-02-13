using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsApp.Models.Articles;
using NewsApp.Models.Comments;
using NewsApp.Services.Articles;
using NewsApp.Services.Comments;
using System.Security.Claims;

namespace NewsApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsService commentsService;
        private readonly IArticlesService articlesService;

        public CommentsController(ICommentsService commentsService, IArticlesService articlesService)
        {
            this.commentsService = commentsService;
            this.articlesService = articlesService;
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody]CommentsInputModel commentModel)
        {
            var userId = this.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var comment = await commentsService.AddAsync<DisplayCommentsViewModel>(commentModel, userId);
            return Ok(comment);
        }

        public IActionResult Get([FromQuery]string articleId)
        {
            return Ok(commentsService.GetAllForArticle<DisplayCommentsViewModel>(Guid.Parse(articleId)));
        }
    }
}
