using NewsApp.Models.Categories;

namespace NewsApp.Services.Categories
{
    public interface ICategoriesService
    {
        IEnumerable<T> GetAll<T>();
        Task<bool> ExistsByIdAsync(string id);
    }
}
