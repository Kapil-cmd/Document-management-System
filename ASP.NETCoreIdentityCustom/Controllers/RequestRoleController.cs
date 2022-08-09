using Microsoft.AspNetCore.Mvc;
using ASP.NETCoreIdentityCustom.Entity;
using ASP.NETCoreIdentityCustom.Areas.Identity.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ASP.NETCoreIdentityCustom.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using ASP.NETCoreIdentityCustom.Core;

namespace ASP.NETCoreIdentityCustom.Controllers
{
    [Authorize(Policy = Constants.Policies.RequireAdmin)]
    public class RequestRoleController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RequestRoleController(ApplicationDbContext context,SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Role role)
        {
            if (ModelState.IsValid)
            {
                if(role.RequiredRole!=null)
                {
                    role.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var userid = _userManager.GetUserId(HttpContext.User);
                    ApplicationUser user = await _userManager.FindByIdAsync(userid);
                    var firstname = user.FirstName;
                    var lastname = user.LastName;
                    role.Username = firstname + " " + lastname;
                    _context.Add(role);
                    _context.SaveChanges();
                    ViewBag.message = "The Role: |" + role.RequiredRole + "| Request Sent Successfully";
                }
                else
                {
                    ViewBag.message = "You have to select the role";
                    ModelState.AddModelError("", "You cannot submit empty!");
                }
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> PostRequest()
        {
            return View(await _context.RoleRequests.ToListAsync());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostRequest(Role role)
        {
            return View();
        }
     
        public async Task<IActionResult> Delete(int? rowid)
        {
            if (rowid == null)
            {
                return NotFound();
            }
            var Request = await _context.RoleRequests.FirstOrDefaultAsync(m => m.RowId == rowid);

            return View(Request);
        }

  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int rowid)
        {
            var Request = await _context.RoleRequests.FindAsync(rowid);
            _context.RoleRequests.Remove(Request);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(PostRequest));
        }

    }
}
