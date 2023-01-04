using NewsApp.Models.Comments;

namespace NewsApp.Services.Comments
{
    public interface ICommentsService
    {
        ICollection<DisplayCommentsViewModel> GetAllForArticle(Guid articleId);
        Task<ICollection<DisplayCommentsViewModel>> AddAsync(CommentsInputModel commentModel, string userId);
    }
}
