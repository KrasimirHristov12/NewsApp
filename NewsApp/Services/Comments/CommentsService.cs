using Microsoft.AspNetCore.Identity;
using NewsApp.Data;
using NewsApp.Data.Models;
using NewsApp.Models.Comments;

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

        public CommentsViewModel Add(CommentsInputModel commentModel)
        {
            var comment = new Comment()
            {
                ArticleId = Guid.Parse(commentModel.ArticleId),
                UserId = commentModel.UserId,
                Content = commentModel.Content,
            };
            dbContext.Comments.Add(comment);
            dbContext.SaveChanges();
            return new CommentsViewModel
            {
                Content = comment.Content,
                CreatedOn = comment.CreatedOn.ToString("G"),
                UserName = userManager.FindByIdAsync(comment.UserId).GetAwaiter().GetResult().UserName
            };
        }

        public ICollection<CommentsViewModel> GetAllForArticle(Guid articleId)
        {
            return dbContext.Comments
                .Where(c => c.ArticleId == articleId)
                .Select(c => new CommentsViewModel
                {
                    Content = c.Content,
                    CreatedOn = c.CreatedOn.ToString("G"),
                    UserName = c.User.UserName
                }).ToList();

        }
    }
}
