using System.ComponentModel.DataAnnotations;

namespace NewsApp.Data.Models
{
    public class Image
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(6)]
        public string Extension { get; set; }
        

        [Required]
        public Article Article { get; set; }
        public Guid ArticleId { get; set; }
    }
}
