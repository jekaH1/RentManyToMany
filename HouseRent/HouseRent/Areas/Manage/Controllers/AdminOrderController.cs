using HouseRent.Context;
using HouseRent.Helper;
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
        public IActionResult Index(int page=1)
        {
            var query= _appDbContext.Orders.Include(x => x.OrderItems).Include(x => x.Apartment.ApartmentCategory).Include(x => x.Apartment.ApartmentFeatures).Include(x => x.Apartment.ApartmentImages).AsQueryable();
            PaginatedList<Order>orders=PaginatedList<Order>.Create(query,4,page); 
            return View(orders);
        }
    }
}
