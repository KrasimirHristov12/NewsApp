using Microsoft.AspNetCore.Identity;
using NewsApp.Data;
using NewsApp.Data.Models;
using NewsApp.Models.Comments;
using NewsApp.Services.Mapping;

namespace NewsApp.Services.Comments
{
    public class CommentsService : ICommentsService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentsService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public ICollection<DisplayCommentsViewModel> Add(CommentsInputModel commentModel, string userId)
        {
            var comment = new Comment()
            {
                ArticleId = Guid.Parse(commentModel.ArticleId),
                UserId = userId,
                Content = commentModel.Content,
                OuterCommentId = commentModel.OuterCommentId != null ? Guid.Parse(commentModel.OuterCommentId): null


            };
            dbContext.Comments.Add(comment);
            dbContext.SaveChanges();
            return GetAllForArticle(Guid.Parse(commentModel.ArticleId));
        }

        public ICollection<DisplayCommentsViewModel> GetAllForArticle(Guid articleId)
        {
            return dbContext.Comments
                .Where(c => c.ArticleId == articleId)
                .To<DisplayCommentsViewModel>()
                .ToList();

        }
    }
}
