using HouseRent.Context;
using HouseRent.Helper;
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
        public IActionResult Index(int page=1)
        {
            var query= _appDbContext.AdminMessages.AsQueryable();
            PaginatedList<AdminMessage> messages = PaginatedList<AdminMessage>.Create(query, 10, page);
            return View(messages);
        }
    }
}
 