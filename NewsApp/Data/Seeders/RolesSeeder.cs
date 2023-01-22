using Microsoft.AspNetCore.Identity;
using NewsApp.Common;
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
            if (!context.Roles.Any(r => r.Name == WebConstants.Role.AuthorRoleName))
            {
                var authorRole = new IdentityRole
                {
                    Name = WebConstants.Role.AuthorRoleName,
                };
                var result = await roleManager.CreateAsync(authorRole);
            }

            if (!context.Roles.Any(r => r.Name == WebConstants.Role.AdminRoleName))
            {
                var authorRole = new IdentityRole
                {
                    Name = WebConstants.Role.AdminRoleName,
                };
                var result = await roleManager.CreateAsync(authorRole);
            }
        }
    }
}
