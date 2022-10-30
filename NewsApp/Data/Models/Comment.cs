using System.ComponentModel.DataAnnotations;

namespace NewsApp.Data.Models
{
    public class Comment
    {
        public Comment()
        {
            CreatedOn = DateTime.UtcNow;
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
        public Guid ArticleId { get; set; }
        [Required]
        public Article Article { get; set; }
    }
}
