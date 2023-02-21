using HouseRent.Context;
using HouseRent.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HouseRent.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AdminOrderController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public AdminOrderController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IActionResult Index()
        {
            List<Order>orders=_appDbContext.Orders.Include(x=>x.OrderItems).Include(x=>x.Apartment.ApartmentCategory).Include(x => x.Apartment.ApartmentFeatures).Include(x => x.Apartment.ApartmentImages).ToList();
            return View(orders);
        }
    }
}
