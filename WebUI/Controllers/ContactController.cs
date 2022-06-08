using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class ContactController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IContactFormService _contactFormService;
        public ContactController(ICategoryService categoryService, IContactFormService contactFormService)
        {
            _categoryService = categoryService;
            _contactFormService = contactFormService;
        }

        public async Task<IActionResult>Index()
        {
            ViewBag.catList=await _categoryService.GetCategories();
            return View();
        }

        [HttpPost]
        public async  Task<IActionResult> Index(ContactForm contact)
        {
            ViewBag.catList = await _categoryService.GetCategories();
            if (ModelState.IsValid)
            {
                try
                {
                    await _contactFormService.Add(contact);
                    return Ok(new { status=200});
                }
                catch (Exception err)
                {
                   return BadRequest(err.Message);
                }
            }
            return BadRequest(new {status=401});
        }
    }
}
