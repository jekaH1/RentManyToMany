using HouseRent.Context;
using HouseRent.Helper;
using HouseRent.Models;
using HouseRent.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HouseRent.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]

    public class AdminOrderController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IEmailService _emailService;

        public AdminOrderController(AppDbContext appDbContext,IEmailService emailService)
        {
            _appDbContext = appDbContext;
            _emailService = emailService;
        }
        public IActionResult Index(int page=1)
        {
            var query= _appDbContext.Orders.Include(x => x.OrderItems).Include(x => x.Apartment.ApartmentCategory).Include(x => x.Apartment.ApartmentFeatures).Include(x => x.Apartment.ApartmentImages).Where(x=>x.OrderStatus==0).AsQueryable();
            PaginatedList<Order>orders=PaginatedList<Order>.Create(query,4,page); 
            return View(orders);
        }
        public IActionResult Detail(int id)
        {
            Order order = _appDbContext.Orders.Include(x => x.OrderItems).Include(x => x.Apartment.ApartmentCategory).Include(x => x.Apartment.ApartmentFeatures).Include(x => x.Apartment.ApartmentImages).FirstOrDefault(x => x.Id == id);
            if (order == null) { return NotFound(); }
            return View(order);
        }
        
        public IActionResult Accept(int id) 
        {
            Order order = _appDbContext.Orders.Include(x => x.OrderItems).Include(x => x.Apartment.ApartmentCategory).Include(x => x.Apartment.ApartmentFeatures).Include(x => x.Apartment.ApartmentImages).FirstOrDefault(x => x.Id == id);
            if (order == null) { return NotFound(); }
            //_emailService.Send(order.eMail, "Rent House", "Your Reservation has been accepted,Have a good vacation! ");
            order.OrderStatus = Enum.OrderStatus.Accepted;
            _appDbContext.SaveChanges();
            return RedirectToAction("index");           
        }
        public IActionResult AcceptedIndex(int id,int page=1) 
        {
            var query = _appDbContext.Orders.Include(x => x.OrderItems).Include(x => x.Apartment).Include(x => x.Apartment.ApartmentCategory).Include(x => x.Apartment.ApartmentFeatures).Include(x => x.Apartment.ApartmentImages).Where(x => x.OrderStatus == Enum.OrderStatus.Accepted).AsQueryable();
            PaginatedList<Order> orders = PaginatedList<Order>.Create(query, 4, page);
            return View(orders);
        }
        public IActionResult End(int id)
        {
            Order order = _appDbContext.Orders.Include(x => x.OrderItems).Include(x => x.Apartment).Include(x => x.Apartment.ApartmentCategory).Include(x => x.Apartment.ApartmentFeatures).Include(x => x.Apartment.ApartmentImages).FirstOrDefault(x => x.Id == id);
            if (order == null) { return NotFound(); }
            order.IsOver=true;
            _appDbContext.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Reject(int id)
        {
            Order order = _appDbContext.Orders.Include(x => x.OrderItems).Include(x => x.Apartment).Include(x => x.Apartment.ApartmentCategory).Include(x => x.Apartment.ApartmentFeatures).Include(x => x.Apartment.ApartmentImages).FirstOrDefault(x => x.Id == id);
            if (order == null) { return NotFound(); }
          
            return View(order);
        }
        [HttpPost]
        public IActionResult Reject(Order newOrder)
        {
            Order exOrder= _appDbContext.Orders.Include(x => x.OrderItems).Include(x=>x.Apartment).Include(x => x.Apartment.ApartmentCategory).Include(x => x.Apartment.ApartmentFeatures).Include(x => x.Apartment.ApartmentImages).FirstOrDefault(x => x.Id == newOrder.Id);
            newOrder.Apartment=exOrder.Apartment;
            newOrder.Apartment.ApartmentCategory = exOrder.Apartment.ApartmentCategory;
            newOrder.Apartment.ApartmentImages = exOrder.Apartment.ApartmentImages;
            newOrder.Apartment.ApartmentFeatures = exOrder.Apartment.ApartmentFeatures;
            newOrder.Fullname = exOrder.Fullname;
            if (!ModelState.IsValid) { return View(newOrder); }
            if (exOrder == null) { return NotFound(); }
            if (newOrder.DeleteMessage is null)
            {
                ModelState.AddModelError("DeleteMessage", "Enter Message");
                return View(newOrder);
            }
            //_emailService.Send(exOrder.eMail, "Rent House", "Sorry,Your Reservation has been Rejected...For further detail check your account!");
            exOrder.DeleteMessage= newOrder.DeleteMessage;
            exOrder.OrderStatus = Enum.OrderStatus.Rejected;
            _appDbContext.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult RejectedOrderIndex(int page=1)
        {
            var query = _appDbContext.Orders.Include(x => x.OrderItems).Include(x => x.Apartment).Include(x => x.Apartment.ApartmentCategory).Include(x => x.Apartment.ApartmentFeatures).Include(x => x.Apartment.ApartmentImages).Where(x => x.OrderStatus == Enum.OrderStatus.Rejected).AsQueryable();
            PaginatedList<Order> orders = PaginatedList<Order>.Create(query, 4, page);
            return View(orders);
        }
    }
}
