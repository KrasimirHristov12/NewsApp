using Microsoft.AspNetCore.Identity;

namespace NewsApp.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Articles = new HashSet<Article>();
            Comments = new HashSet<Comment>();
        }
        public ICollection<Article> Articles { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
