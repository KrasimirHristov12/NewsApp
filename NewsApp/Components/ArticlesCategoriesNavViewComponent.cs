using Microsoft.AspNetCore.Mvc;
using NewsApp.Models.Categories;
using NewsApp.Services.Categories;

namespace NewsApp.Components
{
    public class ArticlesCategoriesNavViewComponent : ViewComponent
    {
        private readonly ICategoriesService categoriesService;

        public ArticlesCategoriesNavViewComponent(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }
        public IViewComponentResult Invoke()
        {
            var categoriesModel = categoriesService.GetAll<CategoriesViewModel>();
            return View(categoriesModel);
        }
    }
}
