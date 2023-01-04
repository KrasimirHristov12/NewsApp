using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsApp.Data;
using NewsApp.Data.Models;
using NewsApp.Models.Likes;
using NewsApp.Services.Likes;
using System.Security.Claims;

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
        [Authorize]
        public async Task<IActionResult> Post(LikesInputModel model)
        {
            string userId = this.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var likeModel = await likesService.CreateLikeAsync(model, userId);
            if (likeModel == null)
            {
                return BadRequest();
            }
            return Ok(likeModel);

        }
    }
}
