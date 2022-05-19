using Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.ComparAdmin.Controllers
{
    [Area(nameof(ComparAdmin))]
    [Authorize("Admin")]

    public class ContactFormController : Controller
    {
        private readonly IContactFormService _service;

        public ContactFormController(IContactFormService service)
        {
            _service = service;
        }

        // GET: ContactFormController
        public async Task<IActionResult> Index()
        {
            var contacts= await _service.GetContactForms();
            return View(contacts);
        }

        // GET: ContactFormController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if(id==null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            var contact= await _service.GetContactFormById(id);
            if (contact == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            contact.IsRead = true;
            await _service.Update(contact);
            return View(contact);
        }

        // GET: ContactFormController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            var contact =await _service.GetContactFormById(id);
            if (contact == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            return View(contact);
        }

        // POST: ContactFormController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int? id)
        {
            try
            {
                if (id == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
                var contact = await _service.GetContactFormById(id);
                if (contact == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
                contact.IsDeleted = true;
                await _service.Update(contact);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
