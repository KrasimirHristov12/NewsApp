using Microsoft.AspNetCore.Identity;

namespace NewsApp.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Articles = new HashSet<Article>();
        }
        public ICollection<Article> Articles { get; set; }
    }
}
