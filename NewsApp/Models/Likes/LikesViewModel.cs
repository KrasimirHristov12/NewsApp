namespace NewsApp.Models.Likes
{
    public class LikesViewModel
    {
        public string ArticleId { get; set; }
        public string UserId { get; set; }
        public int LikesCount { get; set; }
        public int DislikeCount { get; set; }
        public bool? IsLiked { get; set; }
    }
}
