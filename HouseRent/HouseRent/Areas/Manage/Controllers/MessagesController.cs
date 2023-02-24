using HouseRent.Context;
using HouseRent.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HouseRent.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class MessagesController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public MessagesController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IActionResult Index()
        {
            List<AdminMessage> messages = _appDbContext.AdminMessages.ToList();
            return View(messages);
        }
    }
}
 