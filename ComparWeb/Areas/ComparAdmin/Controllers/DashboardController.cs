using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.ComparAdmin.Controllers
{
    public class DashboardController : Controller
    {
        [Area(nameof(ComparAdmin))]

        public IActionResult Index()
        {
            return View();
        }
    }
}
