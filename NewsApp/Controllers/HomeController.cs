using Microsoft.AspNetCore.Mvc;
using NewsApp.Models;
using NewsApp.Services.Football;
using System.Diagnostics;

namespace NewsApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFootballService footballService;

        public HomeController(ILogger<HomeController> logger, IFootballService footballService)
        {
            _logger = logger;
            this.footballService = footballService;
        }

        public IActionResult Index()
        {
            var result = footballService.GetStandingObjectAsync(2022, 39);
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}