namespace NewsApp.Models.Likes
{
    public class LikesInputModel
    {

        public string UserId { get; set; }
        public string ArticleId { get; set; }
        public bool? IsLiked { get; set; }
    }
}
