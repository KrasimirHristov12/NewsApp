using NewsApp.Models.Comments;

namespace NewsApp.Services.Comments
{
    public interface ICommentsService
    {
        ICollection<DisplayCommentsViewModel> GetAllForArticle(Guid articleId);
        ICollection<DisplayCommentsViewModel> Add(CommentsInputModel commentModel);
    }
}
