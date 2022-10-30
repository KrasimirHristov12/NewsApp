using NewsApp.Models.Comments;

namespace NewsApp.Services.Comments
{
    public interface ICommentsService
    {
        ICollection<CommentsViewModel> GetAllForArticle(Guid articleId);
        CommentsViewModel Add(CommentsInputModel commentModel);
    }
}
