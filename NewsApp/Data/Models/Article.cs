namespace NewsApp.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Article
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public Category Category { get; set; }

        public Guid CategoryId { get; set; }
    }
}
