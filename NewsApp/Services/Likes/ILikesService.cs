using NewsApp.Models.Likes;
using System.Security.Claims;

namespace NewsApp.Services.Likes
{
    public interface ILikesService
    {
        LikesViewModel GetAllLikesForArticle(string articleId);
        LikesViewModel CreateLike(LikesInputModel model);
        LikesViewModel UpdateLike(LikesInputModel model);
        bool DidUserVoteForArticle(string articleId, string userId);
    }
}
