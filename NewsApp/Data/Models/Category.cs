using System.ComponentModel.DataAnnotations;

namespace NewsApp.Data.Models
{
    public class Category
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
