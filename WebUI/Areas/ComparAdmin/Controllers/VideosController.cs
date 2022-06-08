using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.ComparAdmin.Controllers
{
    [Area(nameof(ComparAdmin))]
    [Authorize("Admin")]
    public class VideosController : Controller
    {
       private readonly IVideoService _service;
        private readonly ICourseTopicService _courseTopicService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public VideosController(IVideoService service, ICourseTopicService courseTopicService, IWebHostEnvironment webHostEnvironment)
        {
            _service = service;
            _courseTopicService = courseTopicService;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: VideosController
        public async Task<IActionResult> Index()
        {
            var videos=await _service.GetVideos();
            return View(videos);
        }

        // GET: VideosController/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if(id == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            var video= await _service.GetByIdVideos(id);
            if(video == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            return View(video);
        }

        // GET: VideosController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.courseTopicList =await _courseTopicService.GetCourseTopics();
            return View();
        }

        // POST: VideosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Video video, IFormFile VideoUrl, IFormFile PhotoUrl)
        {
            ViewBag.courseTopicList =await _courseTopicService.GetCourseTopics();
            if (ModelState.IsValid)
            {
                try
                {
                    if (VideoUrl != null && PhotoUrl != null)
                    {
                        string uniqueFileName = UploadedFile(VideoUrl);
                        string uniqueFileNameTwo = UploadedFile(PhotoUrl);
                        video.VideoUrl = "/uploads/" + uniqueFileName;
                        video.PhotoUrl = "/uploads/" + uniqueFileNameTwo;
                        _service.AddVideos(video);
                        return RedirectToAction("Index");
                    }
                    return View(video);
                }
                catch
                {
                    return RedirectToAction("Index");
                }
            }
           return View(video);  
        }

        // GET: VideosController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.courseTopicList =await _courseTopicService.GetCourseTopics();
            if (id == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            var video = await _service.GetByIdVideos(id);
            if (video == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            return View(video);
        }

        // POST: VideosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Video video, IFormFile? newVideoUrl,IFormFile? newPhotoUrl)
        {
            ViewBag.courseTopicList = await _courseTopicService.GetCourseTopics();
            try
            {
                if (newVideoUrl != null)
                {
                    string uniqueFileName = UploadedFile(newVideoUrl);
                    video.VideoUrl = "/uploads/" + uniqueFileName;

                }
                if (newPhotoUrl != null)
                {
                    string uniqueFileName = UploadedFile(newPhotoUrl);
                    video.PhotoUrl = "/uploads/" + uniqueFileName;


                }
                await _service.UpdateVideos(video);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(video);
            }
        }

        // GET: VideosController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            var video = await _service.GetByIdVideos(id);
            if (video == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
            return View(video);
        }

        // POST: VideosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(int? id)
        {
            try
            {
                if (id == null) return RedirectToAction("ErrorPage", "Home", new { area = " " });
                var video = await _service.GetByIdVideos(id);
                video.IsDeleted = true;
                await _service.UpdateVideos(video);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //UploadHelper
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
