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
        public IEnumerable<CategoriesViewModel> GetAll()
        {
            return categoryRepo.GetAll<Category>()
                .To<CategoriesViewModel>()
                .ToList();
           
        }
    }
}
