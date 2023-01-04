using NewsApp.Data.Models;
using NewsApp.Services.Mapping;

namespace NewsApp.Models.Categories
{
    public class CategoriesViewModel : IMapFrom<Category>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
