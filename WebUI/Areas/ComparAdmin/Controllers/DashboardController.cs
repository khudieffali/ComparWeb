using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.ComparAdmin.Controllers
{
    public class DashboardController : Controller
    {
        [Area(nameof(ComparAdmin))]
        [Authorize("Admin")]

        public IActionResult Index()
        {
            return View();
        }
    }
}
