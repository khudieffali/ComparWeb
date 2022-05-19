using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.ComparAdmin.Controllers
{
    [Area(nameof(ComparAdmin))]
    [Authorize("Admin")]    
    public class SkillsController : Controller
    {
        private readonly ISkillService _service;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SkillsController(ISkillService service, IWebHostEnvironment webHostEnvironment)
        {
            _service = service;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: SkillsController
        public async Task<IActionResult> Index()
        {
            var skills=await _service.GetSkills();
            return View(skills);
        }

        // GET: SkillsController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if(id==null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            var skill=await _service.GetByIdSkill(id);
            if(skill==null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            return View(skill);
        }

        // GET: SkillsController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: SkillsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Skill skill,IFormFile MainPhoto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (MainPhoto != null)
                    {
                        string uniqueFileName = UploadedFile(MainPhoto);
                        skill.MainPhoto = "/uploads/" + uniqueFileName;
                        await _service.AddSkill(skill);
                        return RedirectToAction(nameof(Index));
                    }
                    return View(skill);
                }
                catch
                {
                    return View(skill);
                }
            }
          return View(skill);
        }

        // GET: SkillsController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            var skill = await _service.GetByIdSkill(id);
            if (skill == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            return View(skill);
        }

        // POST: SkillsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Skill skill,IFormFile? newMain)
        {
            if(id!=skill.Id) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            if (ModelState.IsValid)
            {
                try
                {
                    if(newMain != null)
                    {
                        string uniqueFileName = UploadedFile(newMain);
                        skill.MainPhoto = "/uploads/" + uniqueFileName;
                    }
                    await _service.UpdateSkill(skill);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View(skill);
                }
            }
           return View(skill);
        }

        // GET: SkillsController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            var skill = await _service.GetByIdSkill(id);
            if (skill == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            return View(skill);
        }

        // POST: SkillsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(int? id)
        {
            try
            {
                if (id == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
                var skill = await _service.GetByIdSkill(id);
                if (skill == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
                skill.IsDeleted = true;
                await _service.UpdateSkill(skill);
                return RedirectToAction(nameof(Index));
            }

            catch
            {
                return View();
            }
        }
        //upload file
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
