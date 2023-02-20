using HouseRent.Context;
using HouseRent.Helper;
using HouseRent.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HouseRent.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]

    public class SliderController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _env;

        public SliderController(AppDbContext appDbContext, IWebHostEnvironment env)
        {
            _appDbContext = appDbContext;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Slider> sliders = _appDbContext.Sliders.Where(x => x.IsDeleted == false).ToList();
            return View(sliders);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Slider slider)
        {
            if (!ModelState.IsValid) { return View(slider); }
            if (slider.Image is null)
            {
                ModelState.AddModelError("Image", "Required");
                return View(slider);
            }
            if (slider.Image.ContentType != "image/jpeg" && slider.Image.ContentType != "image/png")
            {
                ModelState.AddModelError("Image", "Only PNG and JPEG files allowed");
                return View(slider);
            }
            if (slider.Image.Length > 2097152)
            {
                ModelState.AddModelError("Image", "Up to 2mb files allowed");
                return View(slider);
            }
            slider.Img = slider.Image.SaveFile(_env.WebRootPath, "uploads/sliders");
            _appDbContext.Add(slider);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            Slider slider = _appDbContext.Sliders.FirstOrDefault(s => s.Id == id);
            if (slider is null) { return NotFound(); }
            string path = Path.Combine(_env.WebRootPath, "uploads/sliders", slider.Img);
            System.IO.File.Delete(path);
            _appDbContext.Remove(slider);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            Slider slider = _appDbContext.Sliders.FirstOrDefault(x => x.Id == id);
            if (slider is null) { return NotFound(); }
            return View(slider);
        }

        [HttpPost]
        public IActionResult Update(Slider newSlider)
        {
            if (!ModelState.IsValid) { return View(newSlider); }
            Slider exSlider = _appDbContext.Sliders.FirstOrDefault(x => x.Id == newSlider.Id);
            if (exSlider is null) { return NotFound(); }

            if (newSlider.Image != null)
            {
                if (newSlider.Image.ContentType != "image/jpeg" && newSlider.Image.ContentType != "image/png")
                {
                    ModelState.AddModelError("Image", "Only PNG and JPEG files allowed");
                    return View(newSlider);
                }
                if (newSlider.Image.Length > 2097152)
                {
                    ModelState.AddModelError("Image", "Up to 2mb files allowed");
                    return View(newSlider);
                }
                string path = Path.Combine(_env.WebRootPath, "uploads/settings", exSlider.Img);
                if (System.IO.File.Exists(path))
                {

                    System.IO.File.Delete(path);
                }
                exSlider.Img = newSlider.Image.SaveFile(_env.WebRootPath, "uploads/settings");
            }
            exSlider.Description = newSlider.Description;
            exSlider.Title1 = newSlider.Title1;
            exSlider.Title2 = newSlider.Title2;
            exSlider.RedirectUrl = newSlider.RedirectUrl;
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult IsDeletedIndex()
        {
            List<Slider> sliders = _appDbContext.Sliders.Where(x => x.IsDeleted == true).ToList();
            return View(sliders);
        }

        public IActionResult SoftDelete(int id)
        {
            Slider slider = _appDbContext.Sliders.FirstOrDefault(x => x.Id == id);
            if (slider is null) { return NotFound(); }
            slider.IsDeleted = true;
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult SoftDeletedIndex()
        {
            List<Slider> sliders = _appDbContext.Sliders.Where(x => x.IsDeleted == true).ToList();
            return View(sliders);
        }
        public IActionResult Restore(int id)
        {
            Slider slider = _appDbContext.Sliders.FirstOrDefault(x => x.Id == id);
            if (slider is null) { return NotFound(); }
            slider.IsDeleted = false;
            _appDbContext.SaveChanges();
            return RedirectToAction("SoftDeletedIndex");
        }

    }
}
