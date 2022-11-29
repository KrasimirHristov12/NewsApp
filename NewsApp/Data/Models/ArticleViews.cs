using System.ComponentModel.DataAnnotations;

namespace NewsApp.Data.Models
{
    public class ArticleViews
    {
        public int Id { get; set; }
        [Required]
        public Article Article { get; set; }
        public Guid ArticleId { get; set; }

        public int ViewsCount { get; set; }
    }
}
