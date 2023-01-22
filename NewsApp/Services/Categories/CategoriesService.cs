using NewsApp.Data;
using NewsApp.Data.Models;
using NewsApp.Models.Categories;
using NewsApp.Services.Mapping;

namespace NewsApp.Services.Categories
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IRepository categoryRepo;

        public CategoriesService(IRepository categoryRepo)
        {
            this.categoryRepo = categoryRepo;
        }
        public IEnumerable<T> GetAll<T>()
        {
            return categoryRepo.GetAll<Category>()
                .To<T>()
                .ToList();
           
        }
        public async Task<bool> ExistsByIdAsync(string id)
        {
            var category = await categoryRepo.GetByIdAsync<Category>(id);
            return category != null;
        }
    }
}
