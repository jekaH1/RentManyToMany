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

    public class CategoriesController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _env;

        public CategoriesController(AppDbContext appDbContext,IWebHostEnvironment env)
        {
            _appDbContext = appDbContext;
            _env = env;
        }
        public IActionResult Index()
        {
            List<ApartmentCategory> categories=_appDbContext.ApartmentCategories.ToList();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ApartmentCategory category) 
        {
            if(!ModelState.IsValid) { return View(category); }
            if (category.ImageApart is null)
            {
                ModelState.AddModelError("ImageApart", "Required");
                return View(category);
            }
            if (category.ImageApart.ContentType != "image/jpeg" && category.ImageApart.ContentType != "image/png")
            {
                ModelState.AddModelError("ImageApart", "Only PNG and JPEG files allowed");
                return View(category);
            }
            if (category.ImageApart.Length > 2097152)
            {
                ModelState.AddModelError("ImageApart", "Up to 2mb files allowed");
                return View(category);
            }
            category.ImgApart = category.ImageApart.SaveFile(_env.WebRootPath, "uploads/categories");
            _appDbContext.Add(category);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            ApartmentCategory category = _appDbContext.ApartmentCategories.FirstOrDefault(x => x.Id == id);
            if(category is null) { return NotFound(); }
            return View(category);
        }

        [HttpPost]
        public IActionResult Update(ApartmentCategory newApartmentCategory)
        {
            if (!ModelState.IsValid) { return View(newApartmentCategory); }
            ApartmentCategory exCategory = _appDbContext.ApartmentCategories.FirstOrDefault(x => x.Id == newApartmentCategory.Id);
            if (exCategory is null) { return NotFound(); }
            if(newApartmentCategory.ImageApart != null)
            {
                if (newApartmentCategory.ImageApart.ContentType != "image/jpeg" && newApartmentCategory.ImageApart.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageApart", "Only PNG and JPEG files allowed");
                    return View(newApartmentCategory);
                }
                if (newApartmentCategory.ImageApart.Length > 2097152)
                {
                    ModelState.AddModelError("ImageApart", "Up to 2mb files allowed");
                    return View(newApartmentCategory);
                }
                string path = Path.Combine(_env.WebRootPath, "uploads/categories", exCategory.ImgApart);
                System.IO.File.Delete(path);
                exCategory.ImgApart = newApartmentCategory.ImageApart.SaveFile(_env.WebRootPath, "uploads/categories");
            }
            exCategory.AppartmentCategoryName = newApartmentCategory.AppartmentCategoryName;
            _appDbContext.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            ApartmentCategory category = _appDbContext.ApartmentCategories.FirstOrDefault(x => x.Id == id);
            if (category is null) { return NotFound(); }
            string path = Path.Combine(_env.WebRootPath, "uploads/sliders", category.ImgApart);
            System.IO.File.Delete(path);
            _appDbContext.Remove(category);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
