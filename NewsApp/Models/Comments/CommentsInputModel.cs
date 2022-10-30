using System.ComponentModel.DataAnnotations;

namespace NewsApp.Models.Comments
{
    public class CommentsInputModel
    {
        [Required]
        public string ArticleId { get; set; }
        [Required]
        public string UserId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Content { get; set; }
    }
}
