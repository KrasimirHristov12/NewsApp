using Microsoft.Build.Framework;

namespace NewsApp.Data.Models
{
    public class UserArticleLikes
    {
        public int Id { get; set; }
        public bool? IsLiked { get; set; }
        [Required]
        public ApplicationUser User { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public Article Article { get; set; }
        public Guid ArticleId { get; set; }
    }
}
