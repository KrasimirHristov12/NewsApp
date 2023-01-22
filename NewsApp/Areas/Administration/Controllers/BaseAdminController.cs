using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsApp.Common;

namespace NewsApp.Areas.Administration.Controllers
{

    [Authorize(Roles = WebConstants.Role.AdminRoleName)]
    public class BaseAdminController : Controller
    {

    }
}
