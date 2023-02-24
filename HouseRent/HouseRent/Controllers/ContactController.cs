using HouseRent.Context;
using HouseRent.Models;
using Microsoft.AspNetCore.Mvc;

namespace HouseRent.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public ContactController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(AdminMessage adminMessage)
        {
            if (!ModelState.IsValid) { return View(adminMessage); }
            
            _appDbContext.AdminMessages.Add(adminMessage);
            _appDbContext.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
