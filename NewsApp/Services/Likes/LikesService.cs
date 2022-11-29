﻿using NewsApp.Data;
using NewsApp.Data.Models;
using NewsApp.Models.Likes;
using System.Security.Claims;

namespace NewsApp.Services.Likes
{
    public class LikesService : ILikesService
    {
        private readonly ApplicationDbContext db;

        public LikesService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public LikesViewModel GetAllLikesForArticle(string articleId)
        {
            var likes = db.UserArticleLikes.Where(x => x.ArticleId.ToString() == articleId);
            var likesModel = new LikesViewModel()
            {
                DislikeCount = likes.Where(x => x.IsLiked == null ? false : !x.IsLiked.Value).Count(),
                LikesCount = likes.Where(x => x.IsLiked == null ? false : x.IsLiked.Value).Count(),
                ArticleId = articleId,
               
            };

            return likesModel;
        }

        public LikesViewModel CreateLike(LikesInputModel model) 
        {
            if (DidUserVoteForArticle(model.ArticleId, model.UserId))
            {
                return UpdateLike(model);
            }
            var likeModel = new UserArticleLikes()
            {
                ArticleId = Guid.Parse(model.ArticleId),
                UserId = model.UserId,
                IsLiked = model.IsLiked,
                
            };

            db.UserArticleLikes.Add(likeModel);
            db.SaveChanges();
            return GetAllLikesForArticle(model.ArticleId);

        }

        public LikesViewModel UpdateLike(LikesInputModel model)
        {
            if (!DidUserVoteForArticle(model.ArticleId, model.UserId))
            {
                return null;
            }
            var likeModel = db.UserArticleLikes.First(x => x.ArticleId.ToString() == model.ArticleId && x.UserId == model.UserId);

            if (likeModel.IsLiked == model.IsLiked)
            {
                return null;
            }
            likeModel.IsLiked = model.IsLiked;
            this.db.SaveChanges();
            return GetAllLikesForArticle(model.ArticleId);

        }

        public bool DidUserVoteForArticle(string articleId, string userId)
        {
            return db.UserArticleLikes.Any(x => x.ArticleId.ToString() == articleId && x.UserId == userId);
        }
    }
}