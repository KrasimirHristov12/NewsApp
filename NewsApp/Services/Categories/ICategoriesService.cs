using NewsApp.Models.Categories;

namespace NewsApp.Services.Categories
{
    public interface ICategoriesService
    {
        IEnumerable<CategoriesViewModel> GetAll();
        Task<bool> ExistsByIdAsync(string id);
    }
}
