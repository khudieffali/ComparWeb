using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.ComparAdmin.Controllers
{
    [Area(nameof(ComparAdmin))]
    [Authorize("Admin")]

    public class TagsController : Controller
    {
        private readonly ITagService _service;
        private readonly IArticleService _articleService;
        public TagsController(ITagService service, IArticleService articleService)
        {
            _service = service;
            _articleService = articleService;
        }

        // GET: TagsController
        public async Task<IActionResult> Index()
        {
            var tags=await _service.GetTags();
            return View(tags);
        }

        // GET: TagsController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            var tags = await _service.GetTagById(id);
            if (tags == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            return View(tags);
        }

        // GET: TagsController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: TagsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tag tag)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.Add(tag);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
           return View(tag);
        }

        // GET: TagsController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            var tags = await _service.GetTagById(id);
            if (tags == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            return View(tags);
        }

        // POST: TagsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Tag tag)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.Update(tag);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
           return View(tag);
        }

        // GET: TagsController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            var tags = await _service.GetTagById(id);
            if (tags == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            return View(tags);
        }

        // POST: TagsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(int? id)
        {
            try
            {
                var tags = await _service.GetTagById(id);
                if (tags == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
                tags.IsDeleted = true;
                await _service.Update(tags);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
