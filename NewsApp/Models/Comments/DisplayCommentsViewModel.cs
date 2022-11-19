namespace NewsApp.Models.Comments
{
    public class DisplayCommentsViewModel
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }
        public string CreatedOn { get; set; }
        public string OuterCommentId { get; set; }

        public ICollection<DisplayCommentsViewModel> InnerComments { get; set; }

    }
}
