namespace NewsApp.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Article
    {
        public Article()
        {
            Comments = new HashSet<Comment>();
            CreatedOn = DateTime.UtcNow;
            UserArticleLikes = new HashSet<UserArticleLikes>();
        }
        public Guid Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public Category Category { get; set; }

        public Guid CategoryId { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<UserArticleLikes> UserArticleLikes { get; set; }
    }
}
