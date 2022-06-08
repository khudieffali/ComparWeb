using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class AcademyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
