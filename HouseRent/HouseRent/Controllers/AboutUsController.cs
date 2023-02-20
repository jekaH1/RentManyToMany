using HouseRent.Context;
using HouseRent.Models;
using Microsoft.AspNetCore.Mvc;

namespace HouseRent.Controllers
{
    public class AboutUsController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public AboutUsController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IActionResult Index()
        {
            AboutUS aboutUs = _appDbContext.AboutUs.FirstOrDefault(x=>x.Id==1);
            return View(aboutUs);
        }
    }
}
