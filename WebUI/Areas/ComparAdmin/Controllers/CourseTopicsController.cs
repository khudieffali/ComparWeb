using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.ComparAdmin.Controllers
{
    [Area(nameof(ComparAdmin))]
    [Authorize("Admin")]

    public class CourseTopicsController : Controller
    {
        private readonly ICourseTopicService _service;
        private readonly ICourseService _courseService;

        public CourseTopicsController(ICourseTopicService service, ICourseService courseService)
        {
            _service = service;
            _courseService = courseService;
        }

        // GET: CourseTopicsController
        public async Task<IActionResult> Index()
        {
            var courseTopics = await _service.GetCourseTopics();
            return View(courseTopics);
        }

        // GET: CourseTopicsController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            var courseTopic = await _service.GetByIdCourseTopics(id);
            if (courseTopic == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            return View(courseTopic);
        }

        // GET: CourseTopicsController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.courseList = await _courseService.GetCourses();
            return View();
        }

        // POST: CourseTopicsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseTopic courseTopic)
        {
            ViewBag.courseList = await _courseService.GetCourses();
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.AddCourseTopic(courseTopic);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return RedirectToAction("Index");
                }
            }
            return View(courseTopic);
        }

        // GET: CourseTopicsController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.courseList = await _courseService.GetCourses();
            if (id == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            var courseTopic = await _service.GetByIdCourseTopics(id);
            if (courseTopic == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            return View(courseTopic);
        }

        // POST: CourseTopicsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourseTopic courseTopic)
        {
            ViewBag.courseList = await _courseService.GetCourses();
            if (id != courseTopic.Id)
                return RedirectToAction("ErrorPage", "Home", new { area = " " });
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateCourseTopic(courseTopic);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return RedirectToAction("Index");
                }
            }
            return View(courseTopic);
        }

        // GET: CourseTopicsController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            var courseTopic = await _service.GetByIdCourseTopics(id);
            if (courseTopic == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            return View(courseTopic);
        }

        // POST: CourseTopicsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(int? id)
        {

            try
            {
                if (id == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
                var courseTopic = await _service.GetByIdCourseTopics(id);
                courseTopic.IsDeleted = true;
                await _service.UpdateCourseTopic(courseTopic);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
