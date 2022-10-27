using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NewsApp.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
    }
}
