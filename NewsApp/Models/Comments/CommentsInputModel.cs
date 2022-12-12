using NewsApp.Common;
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
        [StringLength(DataConstants.Comment.ContentMaxLength, MinimumLength = DataConstants.Comment.ContentMinLength)]
        public string Content { get; set; }

        public string? OuterCommentId { get; set; }
    }
}
