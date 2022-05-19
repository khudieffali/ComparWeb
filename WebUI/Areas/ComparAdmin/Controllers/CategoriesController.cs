using AutoMapper;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.ComparAdmin.Controllers
{
    [Area(nameof(ComparAdmin))]
    [Authorize("Admin")]

    public class CategoriesController : Controller
    {
        // GET: CategoryController
        private readonly ICategoryService _service;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CategoriesController(ICategoryService service, IWebHostEnvironment webHostEnvironment)
        {
            _service = service;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _service.GetCategories();
            return View(categories);
        }

        // GET: CategoryController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if(id==null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            var category=await _service.GetCategoryById(id);
            if(category==null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            return View(category);
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category cat,IFormFile PhotoUrl)
        {
            if (ModelState.IsValid)
            {
                try
            {
                    if (PhotoUrl != null)
                    {
                        string uniqueFileName = UploadedFile(PhotoUrl);
                        cat.PhotoUrl = "/uploads/" + uniqueFileName;
                        await _service.Add(cat);
                        return RedirectToAction("Index");
                    }
                    return View(cat);
                 
            }
            catch
            {
                return RedirectToAction("Index");
            }
            }
            return View(cat);
        }

        // GET: CategoryController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            var category = await _service.GetCategoryById(id);
            if (category == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            return View(category);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,Category cat,IFormFile? newPhoto)
        {
            if (id != cat.Id)return NotFound();
            if (ModelState.IsValid)
           {
                try
             {
                    if (newPhoto != null)
                    {
                        string uniqueFileName = UploadedFile(newPhoto);
                        cat.PhotoUrl = "/uploads/" + uniqueFileName;

                    }
                    await _service.Update(cat);
                return RedirectToAction(nameof(Index));
             }
            catch
             {
                return RedirectToAction("Index");
             }
           }
            return View(cat);
        }
        // GET: CategoryController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            var category = await _service.GetCategoryById(id);
            if (category == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            return View(category);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(int? id)
        {
             if (id == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
             var category = await _service.GetCategoryById(id);
             category.IsDeleted = true;
             await _service.Update(category);
             return RedirectToAction("Index");
            
        }
        //UploadHelper
        private string UploadedFile(IFormFile photo)
        {
            string uniqueFileName = null;

            if (photo != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
