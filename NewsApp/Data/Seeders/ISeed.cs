namespace NewsApp.Data.Seeders
{
    public interface ISeed
    {
        Task SeedAsync(ApplicationDbContext context);
    }
}