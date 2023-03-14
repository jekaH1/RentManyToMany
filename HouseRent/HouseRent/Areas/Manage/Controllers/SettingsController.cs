using HouseRent.Context;
using HouseRent.Helper;
using HouseRent.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Configuration;
using System.Data;

namespace HouseRent.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]

    public class SettingsController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _env;

        public SettingsController(AppDbContext appDbContext, IWebHostEnvironment env)
        {
            _appDbContext = appDbContext;
            _env = env;
        }
        public IActionResult Index()
        {
            List<SettingsRH> settings = _appDbContext.SettingsRH.ToList();
            return View(settings);
        }
        public IActionResult ImageIndex()
        {
            List<SettingsRH> settings = _appDbContext.SettingsRH.ToList();
            return View(settings);
        }

        public IActionResult Update(int id)
        {
            SettingsRH settings = _appDbContext.SettingsRH.FirstOrDefault(x => x.Id == id);
            if (settings == null) { return NotFound(); }
            return View(settings);
        }
        [HttpPost]
        public IActionResult Update(SettingsRH newSetting)
        {
            if (!ModelState.IsValid) { return View(newSetting); }
            SettingsRH exSett = _appDbContext.SettingsRH.FirstOrDefault(x => x.Id == newSetting.Id);
            if (exSett == null) { NotFound(); }
            exSett.Value = newSetting.Value;
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult ImageUpdate(int id)
        {
            SettingsRH settings = _appDbContext.SettingsRH.FirstOrDefault(x => x.Id == id);
            if (settings == null) return View("error");
            return View(settings);
        }
        [HttpPost]
        public IActionResult ImageUpdate(SettingsRH setting)
        {
            SettingsRH exstsettings = _appDbContext.SettingsRH.FirstOrDefault(x => x.Id == setting.Id);
            if (exstsettings == null) return View("error");
            if (!ModelState.IsValid) return View(setting);

            if (setting.Image != null)
            {
                if (setting.Image.Length > 2097152)
                {
                    ModelState.AddModelError("Image", "Image size must be 2mb or lower");
                    return View(setting);
                }
                if (setting.Image.ContentType != "image/png" && setting.Image.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("Image", "Image type must be png, jpg or jpeg");
                    return View(setting);
                }
                string path = Path.Combine(_env.WebRootPath, "uploads/settings", exstsettings.Img);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                exstsettings.Img = setting.Image.SaveFile(_env.WebRootPath, "uploads/settings");
            }
            exstsettings.Value = exstsettings.Img;
            _appDbContext.SaveChanges();
            return RedirectToAction("index");
        }



    }
}
