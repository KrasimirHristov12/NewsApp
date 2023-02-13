using NewsApp.Models.Comments;

namespace NewsApp.Services.Comments
{
    public interface ICommentsService
    {
        ICollection<T> GetForArticlePerPage<T>(Guid articleId, int currentPage);
        public ICollection<T> GetAllForArticle<T>(Guid articleId);

        Task<ICollection<T>> AddAsync<T>(CommentsInputModel commentModel, string userId);
        ICollection<T> GetInnerComments<T>(Guid outerCommentId);
        int CommentsCountForArticle(Guid articleId);
        int PagesCountForArticle(Guid articleId);

    }
}
