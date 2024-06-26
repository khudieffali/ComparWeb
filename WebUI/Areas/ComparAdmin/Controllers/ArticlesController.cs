﻿using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.ComparAdmin.Controllers
{
    [Area(nameof(ComparAdmin))]
    [Authorize("Admin")]
    public class ArticlesController : Controller
    {
        private readonly IArticleService _service;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;
        private readonly IArticleImageService _articleImageService;
        private readonly IArticleToTagService _articleToTagService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ArticlesController(IArticleService service, IWebHostEnvironment webHostEnvironment, ICategoryService categoryService, ITagService tagService, IArticleImageService articleImageService, IArticleToTagService articleToTagService)
        {
            _service = service;
            _webHostEnvironment = webHostEnvironment;
            _categoryService = categoryService;
            _tagService = tagService;
            _articleImageService = articleImageService;
            _articleToTagService = articleToTagService;
        }

        // GET: ArticlesController
        public async Task<IActionResult> Index()
        {
            var articles = await _service.GetArticles();
            return View(articles);
        }

        // GET: ArticlesController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return RedirectToAction("ErrorPage", "Home", new { area = " " }); 
            var article = await _service.GetByIdArticle(id);
            if(article == null)return RedirectToAction("ErrorPage", "Home", new { area = " " });
            return View(article);
        }

        // GET: ArticlesController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.catList=await _categoryService.GetCategories();
            ViewBag.tagList=await _tagService.GetTags();
            return View();
        }

        // POST: ArticlesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Article article,IFormFile MainPhoto,IFormFile CoverPhoto, IFormFile[] ArticleImages,int[] TagIds)
        
        {
            ViewBag.catList = await _categoryService.GetCategories();
            ViewBag.tagList = await _tagService.GetTags();
           
            if (ModelState.IsValid)
            {
                article.ArticleToTags = new List<ArticleToTag>();
                try
                {
                    var upd = "/uploads";
                    var rootFile = Path.Combine(_webHostEnvironment.WebRootPath, upd);
                    if (!Directory.Exists(upd))
                    {
                        Directory.CreateDirectory(upd);
                    }
                    else
                    {
                         if(MainPhoto!=null && CoverPhoto!=null && ArticleImages!=null && ArticleImages.Length > 0)
                        {
                            string uniqueFileNameMain = UploadedFile(MainPhoto);
                            string uniqueFileNameCover = UploadedFile(CoverPhoto);
                            article.MainPhoto = "/uploads/" + uniqueFileNameMain;
                            article.CoverPhoto = "/uploads/" + uniqueFileNameCover;
                            foreach (var pic in ArticleImages)
                            {
                                string uniqueFileNamePic = UploadedFile(pic);
                                article.ArticleImages.Add(new ArticleImage { ArticleId = article.Id, ArticleImg = "/uploads/" + uniqueFileNamePic });
                            }
                            foreach (var id in TagIds)
                             {
                                article.ArticleToTags.Add(new ArticleToTag { TagId=id, ArticleId=article.Id });
                            }
                            await _service.AddArticle(article);
                            return Ok(new {Status=200});
                        }
                    }

                    return View(article);
                }
                catch
                {
                    return View(article);
                }
            }
            return View(article);
        }
        // GET: ArticlesController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.catList = await _categoryService.GetCategories();
            ViewBag.tagList = await _tagService.GetTags();
            if (id == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            var article = await _service.GetByIdArticle(id);
            if (article == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            return View(article);
        }

        // POST: ArticlesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? id, Article article, IFormFile? newMainPhoto, IFormFile? newCoverPhoto, IFormFile[] PictureUrlss, string? removePicturesIds, int[]? TagIds,string? oldTags,string? oldPictures)
        {
            ViewBag.catList = await _categoryService.GetCategories();   
            ViewBag.tagList = await _tagService.GetTags();
            if (id != article.Id) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            if (ModelState.IsValid)
            {
                try
                {
                    if (newMainPhoto != null)
                    {
                        string uniqueFileName = UploadedFile(newMainPhoto);
                        article.MainPhoto = "/uploads/" + uniqueFileName;

                    }
                    if (newCoverPhoto != null)
                    {
                        string uniqueFileName = UploadedFile(newCoverPhoto);
                        article.CoverPhoto = "/uploads/" + uniqueFileName;
                    }
                    List<int> rmvPicIds=new();
                    if (removePicturesIds != null)
                    {
                        rmvPicIds = removePicturesIds.Split("-")
                                          .Select(x => int.Parse(x)).ToList();
                       var removeImages=await _articleImageService.GetByIdArticleImages(rmvPicIds,article.Id);
                        await _articleImageService.DeleteArticleImages(removeImages);
                    }
                    List<int> oldPictureIds = oldPictures.Split("-")
                      .Select(x => int.Parse(x))
                      .Where(x => !rmvPicIds.Contains(x)).ToList();
                    var oldPicturewithoutRemove =await _articleImageService.GetByIdArticleImages(oldPictureIds,article.Id);
                    article.ArticleImages = oldPicturewithoutRemove.Count > 0 ? oldPicturewithoutRemove : new List<ArticleImage>();
                    foreach (var arImg in PictureUrlss)
                    {
                        if (arImg != null)
                        {
                            string uniqueFileName = UploadedFile(arImg);
                            article.ArticleImages.Add(new ArticleImage() { ArticleId=article.Id,ArticleImg="/uploads/"+uniqueFileName});
                        }
                    }
                    
                   
                    if (oldTags != null)
                    {
                       int[] oldTagIds = oldTags.Split("-")
                     .Select(x => int.Parse(x)).ToArray();
                        var oldArticleTag = await _articleToTagService.GetArticleTags(TagIds, article.Id);
                        var oldArticleTagsDelete = await _articleToTagService.GetArticleTagsDelete(TagIds, article.Id);
                        await _articleToTagService.DeleteArticleTag(oldArticleTagsDelete);
                        article.ArticleToTags = oldArticleTag.Count > 0 ? oldArticleTag : new List<ArticleToTag>();
                        foreach (var ids in TagIds)
                        {
                            if (!oldTagIds.Contains(ids))
                            {
                                article.ArticleToTags.Add(new ArticleToTag() { ArticleId = article.Id, TagId = ids });
                            }
                        }
                    }
                    else
                    {
                        foreach (var tagId in TagIds)
                        {
                            article.ArticleToTags.Add(new ArticleToTag() { ArticleId = article.Id, TagId = tagId });
                        }
                    }
                    await _service.UpdateArticle(article);
                    return Ok(new {status=200});
                }
                catch
                {
                    return View();
                }
            }
            return View(article);
        }

        // GET: ArticlesController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            var article = await _service.GetByIdArticle(id);
            if (article == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            return View(article);
        }

        // POST: ArticlesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(int? id)
        {
            try
            {
                if (id == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
                var article = await _service.GetByIdArticle(id);
                if (article == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
                article.IsDeleted = true;
                await _service.UpdateArticle(article);
                return RedirectToAction(nameof(Index));
            }
            
            catch
            {
                return View();
            }
        }
        private string UploadedFile(IFormFile photo)
        {
            string uniqueFileName = null;

            if (photo != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                photo.CopyTo(fileStream);
            }
            return uniqueFileName;
        }
    }
}
