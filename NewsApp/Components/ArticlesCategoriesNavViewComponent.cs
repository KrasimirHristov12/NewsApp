using Microsoft.AspNetCore.Mvc;
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
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categoriesModel = categoriesService.GetAll();
            return View(categoriesModel);
        }
    }
}
