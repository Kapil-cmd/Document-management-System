using ASP.NETCoreIdentityCustom.Areas.Identity.Data;
using ASP.NETCoreIdentityCustom.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETCoreIdentityCustom.Controllers
{
    [Authorize]
    [Authorize(Policy = Constants.Policies.RequireAdmin)]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Documents.ToListAsync());
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

        public async Task<IActionResult> Delete(int? id)
        {
            var documentModel = await _context.Documents.FirstOrDefaultAsync(m => m.DocumentId == id);
            if (id == null)
            {
                return NotFound();
            }

            if (documentModel == null)
            {
                return NotFound();
            }

            return View(documentModel);
        }

        // POST: Document/Delete/Admin
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var documentModel = await _context.Documents.FindAsync(id);
            _context.Documents.Remove(documentModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
