using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using WebUI.ViewModels;

namespace WebUI.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ISkillService _skillService;
        public CourseController(ICourseService courseService, ISkillService skillService)
        {
            _courseService = courseService;
            _skillService = skillService;
        }

        public IActionResult CourseList()
        {
            return View();
        }
        public async Task<IActionResult> CourseDetail(int? id)
        {
            if(id == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            var selectedCourse=await _courseService.GetByIdCourses(id);
            if(selectedCourse == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            selectedCourse.ViewCount += 1;
            await _courseService.UpdateCourse(selectedCourse);
            //await _skillService.
            CourseDetailVM vm = new()
            {
                Course= await _courseService.GetByIdCourses(id),
            };
            return View(vm);
        }
    }
}
