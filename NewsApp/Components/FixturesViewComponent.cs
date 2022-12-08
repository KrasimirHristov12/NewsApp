using Microsoft.AspNetCore.Mvc;

namespace NewsApp.Components
{
    public class FixturesViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
