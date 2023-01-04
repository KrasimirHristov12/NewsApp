using NewsApp.Models.Likes;
using System.Security.Claims;

namespace NewsApp.Services.Likes
{
    public interface ILikesService
    {
        LikesViewModel GetAllLikesForArticle(string articleId);
        LikesViewModel CreateLike(LikesInputModel model, string userId);
        LikesViewModel UpdateLike(LikesInputModel model, string userId);
        bool DidUserVoteForArticle(string articleId, string userId);
    }
}
