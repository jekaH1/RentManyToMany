using HouseRent.Context;
using HouseRent.Helper;
using HouseRent.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

namespace HouseRent.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]

    public class MessagesController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public MessagesController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IActionResult Index(int page=1)
        {
            var query= _appDbContext.AdminMessages.AsQueryable();
            PaginatedList<AdminMessage> messages = PaginatedList<AdminMessage>.Create(query, 10, page);
            return View(messages);
        }
        public IActionResult Delete(int id)
        {
            AdminMessage message=_appDbContext.AdminMessages.FirstOrDefault(x => x.Id == id);
            if (message == null) { return NotFound(); }
            _appDbContext.Remove(message);
            _appDbContext.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
 