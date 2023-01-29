using NewsApp.Models.Likes;
using System.Security.Claims;

namespace NewsApp.Services.Likes
{
    public interface ILikesService
    {
        LikesViewModel GetAllLikesForArticle(string articleId, string userId);
        Task<LikesViewModel> CreateLikeAsync(LikesInputModel model, string userId);
        Task<LikesViewModel> UpdateLikeAsync(LikesInputModel model, string userId);
        bool? DidUserVoteForArticle(string articleId, string userId);
    }
}
