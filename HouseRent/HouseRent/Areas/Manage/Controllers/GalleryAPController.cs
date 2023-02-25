using HouseRent.Context;
using HouseRent.Helper;
using HouseRent.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Extensions;

namespace HouseRent.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class GalleryAPController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _env;

        public GalleryAPController(AppDbContext appDbContext, IWebHostEnvironment env)
        {
            _appDbContext = appDbContext;
            _env = env;
        }
        public IActionResult Index()
        {
            List<GalleryImages> images = _appDbContext.GalleryImages.ToList();
            return View(images);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(GalleryImages gallery)
        {
            if (!ModelState.IsValid) { return View(gallery); }
            if (gallery.Image is null)
            {
                ModelState.AddModelError("Image", "Required");
                return View(gallery);
            }
            if (gallery.Image.ContentType != "image/jpeg" && gallery.Image.ContentType != "image/png")
            {
                ModelState.AddModelError("Image", "Only PNG and JPEG files allowed");
                return View(gallery);
            }
            if (gallery.Image.Length > 2097152)
            {
                ModelState.AddModelError("Image", "Up to 2mb files allowed");
                return View(gallery);
            }
            gallery.Img = gallery.Image.SaveFile(_env.WebRootPath, "uploads/gallery");

            _appDbContext.GalleryImages.Add(gallery);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            GalleryImages Image = _appDbContext.GalleryImages.FirstOrDefault(x => x.Id == id);
            if (Image == null) { return NotFound(); }
            return View(Image);
        }
        [HttpPost]
        public IActionResult Update(GalleryImages newImage)
        {
            if (!ModelState.IsValid) { return View(newImage); }
            GalleryImages exGalery = _appDbContext.GalleryImages.FirstOrDefault(x => x.Id == newImage.Id);
            if (exGalery == null) { return NotFound(newImage.Id); }

            if (newImage.Image != null)
            {
                if (newImage.Image.ContentType != "image/jpeg" && newImage.Image.ContentType != "image/png")
                {
                    ModelState.AddModelError("Image", "Only PNG and JPEG files allowed");
                    return View(newImage);
                }
                if (newImage.Image.Length > 2097152)
                {
                    ModelState.AddModelError("Image", "Up to 2mb files allowed");
                    return View(newImage);
                }

                string path = Path.Combine(_env.WebRootPath, "uploads/gallery", exGalery.Img);
                if (System.IO.File.Exists(path))
                {

                    System.IO.File.Delete(path);
                }
                exGalery.Img = newImage.Image.SaveFile(_env.WebRootPath, "uploads/gallery");
            }
            exGalery.Href = newImage.Href;
            _appDbContext.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            GalleryImages Image = _appDbContext.GalleryImages.FirstOrDefault(x => x.Id == id);
            if (Image == null) { return NotFound(); }
            _appDbContext.Remove(Image);
            _appDbContext.SaveChanges();
            return RedirectToAction("index");
        }

    }
}
