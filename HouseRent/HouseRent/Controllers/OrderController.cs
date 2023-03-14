using HouseRent.Context;
using HouseRent.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace HouseRent.Controllers
{
    
    public class OrderController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;

        public OrderController(AppDbContext appDbContext,UserManager<AppUser> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            AppUser user = null;
            if(User.Identity.IsAuthenticated is true)
            {
                user=await _userManager.FindByNameAsync(User.Identity.Name);
            }
            List<Order> orders=_appDbContext.Orders.Include(x => x.Apartment).Include(x=>x.Apartment.ApartmentCategory).Include(x=>x.Apartment.ApartmentImages).Where(x=>x.AppUserId==user.Id).ToList();
            return View(orders);
        }
        public async Task<IActionResult> IsOverIndex()
        {
            AppUser user = null;
            if(User.Identity.IsAuthenticated is true)
            {
                user=await _userManager.FindByNameAsync(User.Identity.Name);
            }
            List<Order> orders=_appDbContext.Orders.Include(x => x.Apartment).Include(x=>x.Apartment.ApartmentCategory).Include(x=>x.Apartment.ApartmentImages).Where(x=>x.IsOver==true).Where(x=>x.AppUserId==user.Id).ToList();
            return View(orders);
        }

        public IActionResult OrderDetail(int id)
        {
            Order order = _appDbContext.Orders.Include(x => x.Apartment).Include(x => x.Apartment.ApartmentCategory).Include(x => x.Apartment.ApartmentImages).FirstOrDefault(x => x.Id == id);
            if(order == null) { return NotFound(); }
            return View(order);
        }


        [HttpGet]
        public IActionResult CancellOrder(int id)
        {
            Order order = _appDbContext.Orders.FirstOrDefault(x => x.Id == id);
            if (order == null) { return NotFound(); }
            return View(order);
        }

        [HttpPost]
        public IActionResult CancellOrder(int id,Order newOrder) 
        {
            if(!ModelState.IsValid) { return View(newOrder); }
            Order exorder = _appDbContext.Orders.FirstOrDefault(x => x.Id == id);
            if (exorder == null) { return NotFound(); }
            exorder.DeleteMessage = newOrder.DeleteMessage;
            exorder.IsCancelled=null;
            _appDbContext.SaveChanges();
            return RedirectToAction("index");
        }

        

        
    }
}
