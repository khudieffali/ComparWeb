using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebUI.Models;
using WebUI.ViewModels;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICourseService _courseService;
        private readonly IArticleService _articleService;
        public HomeController(ILogger<HomeController> logger, ICourseService courseService, IArticleService articleService)
        {
            _logger = logger;
            _courseService = courseService;
            _articleService = articleService;
        }

        public async Task<IActionResult> Index()
        {
            HomeVM vm = new()
            {
                CourseList= await _courseService.GetHomeCourses(),
                ArticleList=await _articleService.GetHomeArticles(),
            };
            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult ErrorPage()
        {
            return View();
        }
    }
}