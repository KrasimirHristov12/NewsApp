﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NewsApp.Data;
using NewsApp.Data.Models;
using NewsApp.Models.Comments;
using NewsApp.Services.Mapping;

namespace NewsApp.Services.Comments
{
    public class CommentsService : ICommentsService
    {
        private readonly IRepository repo;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public CommentsService(IRepository repo, UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            this.repo = repo;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<ICollection<T>> AddAsync<T>(CommentsInputModel commentModel, string userId)
        {
            var comment = mapper.Map<Comment>(commentModel);
            comment.UserId = userId;
            await repo.AddAsync(comment);
            return GetAllForArticle<T>(Guid.Parse(commentModel.ArticleId));
        }

        public ICollection<T> GetAllForArticle<T>(Guid articleId)
        {
            return repo.GetAll<Comment>()
                .Where(c => c.ArticleId == articleId)
                .To<T>()
                .ToList();

        }

        public ICollection<T> GetInnerComments<T>(Guid outerCommentId)
        {
            return repo.GetAll<Comment>()
                .Where(c => c.OuterCommentId == outerCommentId)
                .To<T>()
                .ToList();
        }
    }
}
