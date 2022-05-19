using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.ComparAdmin.Controllers
{
    [Area(nameof(ComparAdmin))]
    [Authorize("Admin")]

    public class CoursesController : Controller
    {
        private readonly ICourseService _service;
        private readonly ISkillService _skillService;
        private readonly ICourseToSkillService _courseToSkillService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CoursesController(ICourseService service, IWebHostEnvironment webHost, IWebHostEnvironment webHostEnvironment, ISkillService skillService, ICourseToSkillService courseToSkillService)
        {
            _service = service;
            _webHostEnvironment = webHostEnvironment;
            _skillService = skillService;
            _courseToSkillService = courseToSkillService;
        }

        // GET: CoursesController
        public async Task<IActionResult> Index()
        {
            var courses = await _service.GetCourses();
            return View(courses);
        }

        // GET: CoursesController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)return RedirectToAction("ErrorPage", "Home", new { area = " " });
            var course = await _service.GetByIdCourses(id);
            if (course == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            return View(course);
        }

        // GET: CoursesController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.skillList = await _skillService.GetSkills();
            return View();
        }

        // POST: CoursesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course, IFormFile SlidePhoto, IFormFile MainPhoto, int[] SkillId)
        {
            ViewBag.skillList = await _skillService.GetSkills();

            if (ModelState.IsValid)
            {
                course.CourseToSkills = new List<CourseToSkill>();
                try
                {
                    if (SlidePhoto != null && MainPhoto != null)
                    {
                        string uniqueFileName = UploadedFile(SlidePhoto);
                        string uniqueFileNameTwo = UploadedFile(MainPhoto);
                        course.SlidePhoto = "/uploads/" + uniqueFileName;
                        course.MainPhoto = "/uploads/" + uniqueFileNameTwo;

                        foreach (var id in SkillId)
                        {
                            course.CourseToSkills.Add(new CourseToSkill() { CourseId = course.Id, SkillId = id });
                        }

                        await _service.AddCourse(course);
                        return RedirectToAction("Index");
                    }
                    return View(course);
                }
                catch
                {
                    return View();
                }
            }
            return View(course);
        }

        // GET: CoursesController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.skillList = await _skillService.GetSkills();
            if (id == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            var course = await _service.GetByIdCourses(id);
            if (course == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            return View(course);
        }

        // POST: CoursesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Course course, IFormFile? newSlide, IFormFile? newMain, int[]? SkillIds, string? oldSkill)
        {
            ViewBag.skillList = await _skillService.GetSkills();
            if (ModelState.IsValid)
            {
                try
                {
                    if (newSlide != null)
                    {
                        string uniqueFileName = UploadedFile(newSlide);
                        course.SlidePhoto = "/uploads/" + uniqueFileName;

                    }
                    if (newMain != null)
                    {
                        string uniqueFileName = UploadedFile(newMain);
                        course.MainPhoto = "/uploads/" + uniqueFileName;
                    }
                   
                    if (oldSkill != null)
                    {
                        List<int> oldSkillIds = oldSkill.Split("-")
                        .Select(x => int.Parse(x)).ToList();
                        var oldCourseSkill = await _courseToSkillService.GetCourseToSkill(SkillIds,course.Id);
                        var oldCourseSkillDelete = await _courseToSkillService.GetCourseToSkillDelete(SkillIds, course.Id);
                        await _courseToSkillService.DeleteCourseSkill(oldCourseSkillDelete);
                        course.CourseToSkills = oldCourseSkill.Count > 0 ? oldCourseSkill : new List<CourseToSkill>();
                        foreach (var id in SkillIds)
                        {
                            if (!oldSkillIds.Contains(id))
                            {
                                course.CourseToSkills.Add(new CourseToSkill() { CourseId = course.Id, SkillId = id });
                            }
                        }
                    }
                    else
                    {
                        foreach (var id in SkillIds)
                        {
                            course.CourseToSkills.Add(new CourseToSkill() { CourseId = course.Id, SkillId = id });
                        }
                    }

                    await _service.UpdateCourse(course);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            return View(course);
        }


        // GET: CoursesController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            var course = await _service.GetByIdCourses(id);
            if (course == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            return View(course);
        }

        // POST: CoursesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(int? id)
        {
            if (id == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            var course = await _service.GetByIdCourses(id);
            course.IsDeleted = true;
            await _service.UpdateCourse(course);
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
