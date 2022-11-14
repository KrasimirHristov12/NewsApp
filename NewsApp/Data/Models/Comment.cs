using System.ComponentModel.DataAnnotations;

namespace NewsApp.Data.Models
{
    public class Comment
    {
        public Comment()
        {
            CreatedOn = DateTime.UtcNow;
            InnerComments = new HashSet<Comment>();
        }
        public Guid Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public ApplicationUser User { get; set; }

        public Comment OuterComment { get; set; }
        public Guid? OuterCommentId { get; set; }
        public ICollection<Comment> InnerComments { get; set; }
       

        public Guid ArticleId { get; set; }
        [Required]
        public Article Article { get; set; }
    }
}
