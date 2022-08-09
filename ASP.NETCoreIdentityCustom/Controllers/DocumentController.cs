#nullable disable
using ASP.NETCoreIdentityCustom.Areas.Identity.Data;
using ASP.NETCoreIdentityCustom.Core;
using ASP.NETCoreIdentityCustom.Core.Repositories;
using ASP.NETCoreIdentityCustom.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
namespace ASP.NETCoreIdentityCustom.Controllers
{
    [Authorize]
    public class DocumentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
      
        public DocumentController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment, SignInManager<ApplicationUser>signInManager, UserManager<ApplicationUser> userManager )
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // GET: Document
        public async Task<IActionResult> Index(int? id)
        {
            return View(await _context.Documents.ToListAsync());
        }

        // GET: Document/Details/5
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

        // GET: Document/Create
        [Authorize(Policy = Constants.Policies.RequireWriter)]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Constants.Policies.RequireWriter)]

        public async Task<IActionResult> Create([Bind("DocumentId,UserId,Title,DocumentName,DocumentFiles,DocumentCreaterName,Discription,DocumentCreatedDate,DocumentModifiedDate")] DocumentEntity documentModel)
        {
            //var userName = _userManager.GetUserName(HttpContext.User);
            //documentModel.DocumentCreaterName = userName;
            var userid = _userManager.GetUserId(HttpContext.User);
            ApplicationUser user = await _userManager.FindByIdAsync(userid);
            var firstname = user.FirstName;
            var lastname = user.LastName;
            documentModel.DocumentCreaterName= firstname+ " " + lastname;
            if (ModelState.IsValid)
            {
                if (documentModel.DocumentFiles != null && documentModel.DocumentFiles.Count < 6)
                {
                    documentModel.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    documentModel.DocumentCreatedDate = DateTime.Now;
                    
                    //save image to wwwRootPath
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                        int i = 0;
                    foreach (IFormFile document in documentModel.DocumentFiles)
                    {
                            i = i + 1;
                            if(i==1)
                            {
                                string file = Path.GetFileNameWithoutExtension(document.FileName);
                                string extension = Path.GetExtension(document.FileName);
                                documentModel.Image1Path = file = file + DateTime.Now.ToString("yymmssfff") + extension;
                                string path = Path.Combine(wwwRootPath + "/Image/", file);

                                using (var fileStream = new FileStream(path, FileMode.Create))
                                {
                                    await document.CopyToAsync(fileStream);
                                }
                                _context.Add(documentModel);
                            }
                            if (i == 2)
                            {
                                string file = Path.GetFileNameWithoutExtension(document.FileName);
                                string extension = Path.GetExtension(document.FileName);
                                documentModel.Image2Path = file = file + DateTime.Now.ToString("yymmssfff") + extension;
                                string path = Path.Combine(wwwRootPath + "/Image/", file);

                                using (var fileStream = new FileStream(path, FileMode.Create))
                                {
                                    await document.CopyToAsync(fileStream);
                                }
                                _context.Add(documentModel);
                            }
                            if (i ==3)
                            {
                                string file = Path.GetFileNameWithoutExtension(document.FileName);
                                string extension = Path.GetExtension(document.FileName);
                                documentModel.Image3Path = file = file + DateTime.Now.ToString("yymmssfff") + extension;
                                string path = Path.Combine(wwwRootPath + "/Image/", file);

                                using (var fileStream = new FileStream(path, FileMode.Create))
                                {
                                    await document.CopyToAsync(fileStream);
                                }
                                _context.Add(documentModel);
                            }
                            if (i ==4)
                            {
                                string file = Path.GetFileNameWithoutExtension(document.FileName);
                                string extension = Path.GetExtension(document.FileName);
                                documentModel.Image4Path = file = file + DateTime.Now.ToString("yymmssfff") + extension;
                                string path = Path.Combine(wwwRootPath + "/Image/", file);

                                using (var fileStream = new FileStream(path, FileMode.Create))
                                {
                                    await document.CopyToAsync(fileStream);
                                }
                                _context.Add(documentModel);
                            }
                            if (i==5)
                            {
                                string file = Path.GetFileNameWithoutExtension(document.FileName);
                                string extension = Path.GetExtension(document.FileName);
                                documentModel.Image5Path = file = file + DateTime.Now.ToString("yymmssfff") + extension;
                                string path = Path.Combine(wwwRootPath + "/Image/", file);

                                using (var fileStream = new FileStream(path, FileMode.Create))
                                {
                                    await document.CopyToAsync(fileStream);
                                }
                                _context.Add(documentModel);
                            }
                    }
                       

                    //insert records
                    _context.Add(documentModel);
                    await _context.SaveChangesAsync();
                    TempData["AlertMessage"] = "Document Created Successfully....!!!!";
                        return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "You cannot Upload More Than 5 Images");
                    return View();
                }
            }
            else
            {

                ModelState.AddModelError("", "You have to complete the requirements to post a doocument");
                return View();
            }
            return View(documentModel);
        }

        // GET: Document/Edit/5
         
        public async Task<IActionResult> Edit(int? id)
        {
            var userid=_userManager.GetUserId(HttpContext.User);
            var documentModel = await _context.Documents.FindAsync(id);
            if (documentModel.UserId==userid) 
            {
                if (id == null)
                {
                  return RedirectToAction(nameof(Index));
                    ModelState.AddModelError("", "You cannot access");
                }
                if (documentModel == null)
                {
                  return RedirectToAction(nameof(Index));
                  ModelState.AddModelError("", "You cannot access");
                }
            }
            else
            {
                TempData["Message"] = "You Can Only Edit Your Document....!!!!";
                return RedirectToAction("Index");
                
            }
            return View(documentModel);
        }

        // POST: Document/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("DocumentId, UserId, Title, DocumentName, DocumentFiles, DocumentCreaterName, Discription, DocumentCreatedDate, DocumentModifiedDate")] DocumentEntity documentModel)
        {
            if (ModelState.IsValid)
            {
                if (documentModel.DocumentFiles != null && documentModel.DocumentFiles.Count < 6)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    int i = 0;
                    foreach (IFormFile document in documentModel.DocumentFiles)
                    {

                        i = i + 1;
                        if (i == 1)
                        {
                            string file = Path.GetFileNameWithoutExtension(document.FileName);
                            string extension = Path.GetExtension(document.FileName);
                            documentModel.Image1Path = file = file + DateTime.Now.ToString("yymmssfff") + extension;
                            string path = Path.Combine(wwwRootPath + "/Image/", file);

                            using (var fileStream = new FileStream(path, FileMode.Create))
                            {
                                await document.CopyToAsync(fileStream);
                            }
                            _context.Add(documentModel);
                        }
                        if (i == 2)
                        {
                            string file = Path.GetFileNameWithoutExtension(document.FileName);
                            string extension = Path.GetExtension(document.FileName);
                            documentModel.Image2Path = file = file + DateTime.Now.ToString("yymmssfff") + extension;
                            string path = Path.Combine(wwwRootPath + "/Image/", file);

                            using (var fileStream = new FileStream(path, FileMode.Create))
                            {
                                await document.CopyToAsync(fileStream);
                            }
                            _context.Add(documentModel);
                        }
                        if (i == 3)
                        {
                            string file = Path.GetFileNameWithoutExtension(document.FileName);
                            string extension = Path.GetExtension(document.FileName);
                            documentModel.Image3Path = file = file + DateTime.Now.ToString("yymmssfff") + extension;
                            string path = Path.Combine(wwwRootPath + "/Image/", file);

                            using (var fileStream = new FileStream(path, FileMode.Create))
                            {
                                await document.CopyToAsync(fileStream);
                            }
                            _context.Add(documentModel);
                        }
                        if (i == 4)
                        {
                            string file = Path.GetFileNameWithoutExtension(document.FileName);
                            string extension = Path.GetExtension(document.FileName);
                            documentModel.Image4Path = file = file + DateTime.Now.ToString("yymmssfff") + extension;
                            string path = Path.Combine(wwwRootPath + "/Image/", file);

                            using (var fileStream = new FileStream(path, FileMode.Create))
                            {
                                await document.CopyToAsync(fileStream);
                            }
                            _context.Add(documentModel);
                        }
                        if (i == 5)
                        {
                            string file = Path.GetFileNameWithoutExtension(document.FileName);
                            string extension = Path.GetExtension(document.FileName);
                            documentModel.Image5Path = file = file + DateTime.Now.ToString("yymmssfff") + extension;
                            string path = Path.Combine(wwwRootPath + "/Image/", file);

                            using (var fileStream = new FileStream(path, FileMode.Create))
                            {
                                await document.CopyToAsync(fileStream);
                            }
                            _context.Add(documentModel);
                        }
                    }

                }
                documentModel.Title = new string(documentModel.Title);
                documentModel.Discription = new string(documentModel.Discription);
                documentModel.DocumentName = new string(documentModel.DocumentName);
                documentModel.DocumentCreaterName = new string(documentModel.DocumentCreaterName);
                documentModel.DocumentModifiedDate = DateTime.Now;
                try
                {
                    _context.Update(documentModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentModelExists(documentModel.DocumentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }


                return RedirectToAction(nameof(Index));
            }

            return View(documentModel);
        }

        // GET: Document/Delete/5
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

        // POST: Document/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var documentModel = await _context.Documents.FindAsync(id);
            _context.Documents.Remove(documentModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentModelExists(int id)
        {
            return _context.Documents.Any(e => e.DocumentId == id);
        }

     
       
    }
}
