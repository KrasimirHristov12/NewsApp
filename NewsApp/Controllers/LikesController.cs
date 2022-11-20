using Microsoft.AspNetCore.Mvc;
using NewsApp.Data;
using NewsApp.Data.Models;
using NewsApp.Models.Likes;
using NewsApp.Services.Likes;

namespace NewsApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LikesController : ControllerBase
    {
        private readonly ILikesService likesService;

        public LikesController(ILikesService likesService)
        {
            this.likesService = likesService;
        }
        [HttpPost]
        public IActionResult Post([FromBody]LikesInputModel model)
        {
            var likeModel = likesService.CreateLike(model);
            if (likeModel == null)
            {
                return BadRequest();
            }
            return Ok(likeModel);

        }
    }
}
