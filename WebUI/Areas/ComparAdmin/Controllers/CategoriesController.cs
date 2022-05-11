using AutoMapper;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.ComparAdmin.Controllers
{
    [Area(nameof(ComparAdmin))]
    public class CategoriesController : Controller
    {
        // GET: CategoryController
        ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _service.GetCategories();
            return View(categories);
        }

        // GET: CategoryController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if(id==null) return NotFound();
            var category=await _service.GetCategoryById(id);
            if(category==null) return NotFound();
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
        public async Task<IActionResult> Create(Category cat)
        {
            if (ModelState.IsValid)
            {
                try
            {
                  await _service.Add(cat);
                 return RedirectToAction("Index");
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
            if (id == null) return NotFound();
            var category = await _service.GetCategoryById(id);
            if (category == null) return NotFound();
            return View(category);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Category cat)
        {
            if (id != cat.Id)return NotFound();
            if (ModelState.IsValid)
           {
                try
             {
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
            if (id == null) return NotFound();
            var category = await _service.GetCategoryById(id);
            if (category == null) return NotFound();
            return View(category);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
                    if (id == null) return NotFound();
                    var category = await _service.GetCategoryById(id);
                    category.IsDeleted = true;
                    await _service.Update(category);
                    return RedirectToAction("Index");
            
        }
    
    }
}
