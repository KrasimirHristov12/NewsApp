using Microsoft.AspNetCore.Identity;
using SendGrid.Helpers.Mail;

namespace NewsApp.Data.Seeders
{
    public class RolesSeeder : ISeed
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RolesSeeder(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public async Task SeedAsync(ApplicationDbContext context)
        {
            if (context.Roles.Count() == 0)
            {
                var authorRole = new IdentityRole
                {
                    Name = "Author",
                };
                var result = await roleManager.CreateAsync(authorRole);
            }
        }
    }
}
