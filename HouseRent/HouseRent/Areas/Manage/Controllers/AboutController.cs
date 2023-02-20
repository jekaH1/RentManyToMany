using HouseRent.Context;
using HouseRent.Models;
using Microsoft.AspNetCore.Mvc;
using HouseRent.Helper;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HouseRent.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AboutController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _env;

        public AboutController(AppDbContext appDbContext,IWebHostEnvironment env)
        {
            _appDbContext = appDbContext;
            _env = env;
        }
        public IActionResult Index()
        {
            List<AboutUS> us=_appDbContext.AboutUs.ToList();
            return View(us);
        }
        
        public IActionResult Update(int id)
        {
            AboutUS about =_appDbContext.AboutUs.FirstOrDefault(x=>x.Id== id);
            if (about == null) { return NotFound (); }
            return View(about);
        }
        [HttpPost]
        public IActionResult Update(AboutUS newAbout)
        {
            if (!ModelState.IsValid) { return View(newAbout); }
            AboutUS exAbout = _appDbContext.AboutUs.FirstOrDefault(x => x.Id == newAbout.Id);
            if (exAbout == null) { NotFound(); }
            if (newAbout.AvatarImage != null)
            {
                if (newAbout.AvatarImage.ContentType != "image/jpeg" && newAbout.AvatarImage.ContentType != "image/png")
                {
                    ModelState.AddModelError("AvatarImage", "Only PNG and JPEG files allowed");
                    return View(newAbout);
                }
                if (newAbout.AvatarImage.Length > 2097152)
                {
                    ModelState.AddModelError("AvatarImage", "Up to 2mb files allowed");
                    return View(newAbout);
                }
                string path = Path.Combine(_env.WebRootPath, "uploads/about", exAbout.AvatarImg);
                if (System.IO.File.Exists(path))
                {

                    System.IO.File.Delete(path);
                }
                exAbout.AvatarImg = newAbout.AvatarImage.SaveFile(_env.WebRootPath, "uploads/about");
            }
            if (newAbout.SignImage != null)
            {
                if (newAbout.SignImage.ContentType != "image/jpeg" && newAbout.SignImage.ContentType != "image/png")
                {
                    ModelState.AddModelError("SignImage", "Only PNG and JPEG files allowed");
                    return View(newAbout);
                }
                if (newAbout.SignImage.Length > 2097152)
                {
                    ModelState.AddModelError("SignImage", "Up to 2mb files allowed");
                    return View(newAbout);
                }
                string path = Path.Combine(_env.WebRootPath, "uploads/about", exAbout.SignImg);
                if (System.IO.File.Exists(path))
                {

                    System.IO.File.Delete(path);
                }
                exAbout.SignImg = newAbout.SignImage.SaveFile(_env.WebRootPath, "uploads/about");
            }
            exAbout.Title=newAbout.Title;
            exAbout.Description = newAbout.Description;
            exAbout.CeoName = newAbout.CeoName;
            exAbout.Position=newAbout.Position;
            exAbout.Quote=newAbout.Quote;
            exAbout.PintereseUrl = newAbout.PintereseUrl;
            exAbout.SmthUrl= newAbout.SmthUrl;
            exAbout.GplusUrl=newAbout.GplusUrl;
            exAbout.TwitterUrl=newAbout.TwitterUrl;
            exAbout.FaceUrl=newAbout.FaceUrl;
            
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
