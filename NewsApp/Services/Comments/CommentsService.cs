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

        public ICollection<DisplayCommentsViewModel> Add(CommentsInputModel commentModel)
        {
            var comment = new Comment()
            {
                ArticleId = Guid.Parse(commentModel.ArticleId),
                UserId = commentModel.UserId,
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
                .Select(c => new DisplayCommentsViewModel
                {
                    Id = c.Id.ToString(),
                    Content = c.Content,
                    CreatedOn = c.CreatedOn.ToString("G"),
                    OuterCommentId = c.OuterCommentId.ToString() ?? null,
                    InnerComments = c.InnerComments.Select(ic => new DisplayCommentsViewModel
                    {
                        Id=ic.Id.ToString(),
                        Content=ic.Content,
                        UserName = ic.User.UserName,
                        CreatedOn=ic.CreatedOn.ToString("G"),
                        OuterCommentId = ic.OuterCommentId.ToString() ?? string.Empty,
                        InnerComments = new List<DisplayCommentsViewModel>(),
                        
                    }).ToList(),
                    UserName = c.User.UserName
                }).ToList();

        }

        public ICollection<DisplayCommentsViewModel> GetInnerComments(Guid commentId)
        {
            return dbContext.Comments
                .Where(c => c.OuterCommentId == commentId)
                .Select(c => new DisplayCommentsViewModel
                {
                    Id = c.Id.ToString(),
                    Content = c.Content,
                    CreatedOn = c.CreatedOn.ToString("G"),
                    UserName = c.User.UserName,
                    OuterCommentId = c.OuterCommentId != null ? c.OuterCommentId.ToString() : null,
                }).ToList();
        }
    }
}
