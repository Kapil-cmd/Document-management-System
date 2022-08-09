using ASP.NETCoreIdentityCustom.Areas.Identity.Data;
using ASP.NETCoreIdentityCustom.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETCoreIdentityCustom.Controllers
{
    [Authorize]
    public class RoleViewsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public RoleViewsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;

        }
     
        public async Task<IActionResult> Index(string option, string search)
        {
            if (option == "Title")
            {
                //Index action method will return a view with a student records based on what a user specify the value in textbox  
                return View(_context.Documents.Where(x => x.Title.StartsWith(search) || search == null).ToList());
            }
            if (option == "DocumentName")
            {
                return View(_context.Documents.Where(x => x.DocumentName.StartsWith(search) || search == null).ToList());
            }
            if(option =="DocumentCreaterName")
            {
                return View(_context.Documents.Where(x => x.DocumentCreaterName.StartsWith(search) || search == null).ToList());
            }
            else
            {
                return View(_context.Documents.Where(x => x.DocumentCreaterName.StartsWith(search) || search == null).ToList());

            }
            return View(await _context.Documents.ToListAsync());
        }

        [Authorize(Policy = Constants.Policies.RequireWriter)]
        [HttpGet]
        public ActionResult Write()
        {
            return View();
        }


        [Authorize(Policy = "RequireAdmin")]
        public IActionResult Admin()
        {
            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentModel = await _context.Documents
                .FirstOrDefaultAsync(m => m.DocumentId == id);
            if (documentModel == null)
            {
                return NotFound();
            }

            return View(documentModel);
        }

    }
}

