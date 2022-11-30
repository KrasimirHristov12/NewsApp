using NewsApp.Data.Models;

namespace NewsApp.Data.Seeders
{
    public class CategoriesSeeder : ISeed
    {
        public async Task SeedAsync(ApplicationDbContext context)
        {

            if (!context.Categories.Any())
            {
                for (int i = 1; i <= 10; i++)
                {
                    var category = new Category
                    {
                        Name = $"c{i}",
                    };
                    await context.Categories.AddAsync(category);


                }
                await context.SaveChangesAsync();
            }


        }
    }
}
