using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
namespace Web.ViewComponents
{
    public class CoursesViewComponent:ViewComponent
    {
        private readonly ICourseService _service;

        public CoursesViewComponent(ICourseService service)
        {
            _service = service;
        }

        public ViewViewComponentResult Invoke()
        {
            var selectedCourse = _service.GetCoursesNoAsync();
            return View(selectedCourse);
        }
    }
}
