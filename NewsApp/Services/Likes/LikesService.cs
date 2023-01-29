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
        public LikesViewModel GetAllLikesForArticle(string articleId, string userId)
        {
            var likes = repo.GetAll<UserArticleLikes>().Where(x => x.ArticleId.ToString() == articleId);
            var likesModel = new LikesViewModel()
            {
                DislikeCount = likes.Where(x => x.IsLiked == null ? false : !x.IsLiked.Value).Count(),
                LikesCount = likes.Where(x => x.IsLiked == null ? false : x.IsLiked.Value).Count(),
                ArticleId = articleId,
                UserId = userId,
                IsLiked = likes.Where(x => x.UserId == userId).Count() == 0 ? null : likes.Where(x => x.UserId == userId).First().IsLiked,

            };

            return likesModel;
        }

        public async Task<LikesViewModel> CreateLikeAsync(LikesInputModel model, string userId)
        {
            if (DidUserVoteForArticle(model.ArticleId, userId) != null)
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
            return GetAllLikesForArticle(model.ArticleId, userId);

        }

        public async Task<LikesViewModel> UpdateLikeAsync(LikesInputModel model, string userId)
        {
            if (DidUserVoteForArticle(model.ArticleId, userId) == null)
            {
                return null;
            }
            var likeModel = repo.GetAll<UserArticleLikes>().First(x => x.ArticleId.ToString() == model.ArticleId && x.UserId == userId);

            if (likeModel.IsLiked == model.IsLiked)
            {
                likeModel.IsLiked = null;
            }
            else
            {
                likeModel.IsLiked = model.IsLiked;
            }
            
            await repo.UpdateAsync(likeModel);
            return GetAllLikesForArticle(model.ArticleId, userId);

        }

        public bool? DidUserVoteForArticle(string articleId, string userId)
        {
            var userLike =  repo.GetAll<UserArticleLikes>().FirstOrDefault(x => x.ArticleId.ToString() == articleId && x.UserId == userId);
            if (userLike == null)
            {
                return null;
            }
            return true;
        }
    }
}
