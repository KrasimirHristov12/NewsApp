using NewsApp.Data;
using NewsApp.Data.Models;
using NewsApp.Models.Likes;
using System.Security.Claims;

namespace NewsApp.Services.Likes
{
    public class LikesService : ILikesService
    {
        private readonly IRepository repo;

        public LikesService(IRepository repo)
        {
            this.repo = repo;
        }
        public LikesViewModel GetAllLikesForArticle(string articleId)
        {
            var likes = repo.GetAll<UserArticleLikes>().Where(x => x.ArticleId.ToString() == articleId);
            var likesModel = new LikesViewModel()
            {
                DislikeCount = likes.Where(x => x.IsLiked == null ? false : !x.IsLiked.Value).Count(),
                LikesCount = likes.Where(x => x.IsLiked == null ? false : x.IsLiked.Value).Count(),
                ArticleId = articleId,

            };

            return likesModel;
        }

        public async Task<LikesViewModel> CreateLikeAsync(LikesInputModel model, string userId)
        {
            if (DidUserVoteForArticle(model.ArticleId, userId))
            {
                return await UpdateLikeAsync(model, userId);
            }
            var likeModel = new UserArticleLikes()
            {
                ArticleId = Guid.Parse(model.ArticleId),
                UserId = userId,
                IsLiked = model.IsLiked,

            };

            await repo.AddAsync(likeModel);
            return GetAllLikesForArticle(model.ArticleId);

        }

        public async Task<LikesViewModel> UpdateLikeAsync(LikesInputModel model, string userId)
        {
            if (!DidUserVoteForArticle(model.ArticleId, userId))
            {
                return null;
            }
            var likeModel = repo.GetAll<UserArticleLikes>().First(x => x.ArticleId.ToString() == model.ArticleId && x.UserId == userId);

            if (likeModel.IsLiked == model.IsLiked)
            {
                return null;
            }
            likeModel.IsLiked = model.IsLiked;
            await repo.UpdateAsync(likeModel);
            return GetAllLikesForArticle(model.ArticleId);

        }

        public bool DidUserVoteForArticle(string articleId, string userId)
        {
            return repo.GetAll<UserArticleLikes>().Any(x => x.ArticleId.ToString() == articleId && x.UserId == userId);
        }
    }
}
