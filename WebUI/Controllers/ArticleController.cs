using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using WebUI.ViewModels;

namespace WebUI.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        public ArticleController(IArticleService articleService, ICategoryService categoryService)
        {
            _articleService = articleService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> ArticleList(string? q,int? categoryId)
        {
            ArticleVM vm = new()
            {
                Articles = await _articleService.GetSearchArticles(q,categoryId),
            };
            return View(vm);
        }
        public async Task<IActionResult> ArticleDetails(int? id)
        {
            if(id==null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            var selectedArticle = await _articleService.GetByIdArticle(id);
            if (selectedArticle == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            selectedArticle.ViewCount += 1;
            await _articleService.UpdateArticle(selectedArticle);
            ArticleDetailVM vm = new() {
                Article = await _articleService.GetByIdArticle(id),
                PopularArticles = await _articleService.GetPopularsArticles(),
                NewArticles = await _articleService.GetHomeArticles(),
                Categories=await _categoryService.GetCategories(),
            };

            return View(vm);
        }
    }
}
