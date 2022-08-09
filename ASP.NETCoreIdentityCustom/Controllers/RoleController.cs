using ASP.NETCoreIdentityCustom.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NETCoreIdentityCustom.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy = Constants.Policies.RequireWriter)]
        public IActionResult Write()
        {
            return View();
        }

        //[Authorize(Policy = "RequireAdmin")]
        [Authorize(Roles = $"{Constants.Roles.Administrator},{Constants.Roles.Writer}")]
        public IActionResult Admin()
        {
            return View();
        }
    }
}
