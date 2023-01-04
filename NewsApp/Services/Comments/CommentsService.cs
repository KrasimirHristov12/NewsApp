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

        public CommentsService(IRepository repo, UserManager<ApplicationUser> userManager)
        {
            this.repo = repo;
            this.userManager = userManager;
        }

        public async Task<ICollection<DisplayCommentsViewModel>> AddAsync(CommentsInputModel commentModel, string userId)
        {
            var comment = new Comment()
            {
                ArticleId = Guid.Parse(commentModel.ArticleId),
                UserId = userId,
                Content = commentModel.Content,
                OuterCommentId = commentModel.OuterCommentId != null ? Guid.Parse(commentModel.OuterCommentId): null


            };
            await repo.AddAsync(comment);
            return GetAllForArticle(Guid.Parse(commentModel.ArticleId));
        }

        public ICollection<DisplayCommentsViewModel> GetAllForArticle(Guid articleId)
        {
            return repo.GetAll<Comment>()
                .Where(c => c.ArticleId == articleId)
                .To<DisplayCommentsViewModel>()
                .ToList();

        }
    }
}
