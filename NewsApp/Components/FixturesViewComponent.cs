using Microsoft.AspNetCore.Mvc;

namespace NewsApp.Components
{
    public class FixturesViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
