﻿namespace NewsApp.Data.Models
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

        [Required]
        public Category Category { get; set; }

        public Guid CategoryId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }
    }
}
