namespace NewsApp.Models.Comments
{
    public class CommentsViewModel
    {
        public string UserId { get; set; }
        public string ArticleId { get; set; }
        public ICollection<DisplayCommentsViewModel> Comments { get; set; }
    }
}
