using Microsoft.AspNetCore.Mvc;
using NewsApp.Models.Comments;
using NewsApp.Services.Articles;
using NewsApp.Services.Comments;

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
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]string articleId)
        {
            var article = await articlesService.GetByIdAsync(articleId);
            if (article != null)
            {
                return Ok(commentsService.GetAllForArticle(Guid.Parse(articleId)));
            }
            return NotFound();
            
        }

        [HttpPost]
        public IActionResult Post([FromBody]CommentsInputModel commentModel)
        {
            var comment = commentsService.Add(commentModel);
            return Ok(comment);
        }
    }
}
