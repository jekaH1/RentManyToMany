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

        

    }
}
