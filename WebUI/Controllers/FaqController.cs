using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class FaqController : Controller
    {
        public IActionResult Faq()
        {
            return View();
        }
    }
}
