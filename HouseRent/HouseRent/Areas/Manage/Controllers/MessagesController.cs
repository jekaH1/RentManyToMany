using Microsoft.AspNetCore.Mvc;

namespace HouseRent.Areas.Manage.Controllers
{
    public class MessagesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
