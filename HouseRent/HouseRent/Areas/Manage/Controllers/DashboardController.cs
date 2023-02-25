using HouseRent.Context;
using HouseRent.Models;
using HouseRent.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HouseRent.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]

    public class DashboardController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DashboardController(AppDbContext appDbContext,UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            DashboardViewModel dashboardViewModel = new DashboardViewModel
            {
                Orders = _appDbContext.Orders.Include(x => x.OrderItems).Include(x => x.Apartment.ApartmentCategory).Include(x => x.Apartment.ApartmentFeatures).Include(x => x.Apartment.ApartmentImages).OrderByDescending(x=>x.OrderDay).Where(x=>x.OrderStatus ==0).ToList(),
                ToDoLists=_appDbContext.ToDoListOrders.OrderByDescending(x=>x.todiListDate).Take(4).ToList(),
                AdminMessages=_appDbContext.AdminMessages.OrderByDescending(x=>x.MessageDate).Take(4).ToList(), 
                Orders5= _appDbContext.Orders.Include(x => x.OrderItems).Include(x => x.Apartment.ApartmentCategory).Include(x => x.Apartment.ApartmentFeatures).Include(x => x.Apartment.ApartmentImages).OrderByDescending(x => x.OrderDay).Where(x => x.OrderStatus == 0).Take(5).ToList()
            };

            return View(dashboardViewModel);
        }
        [HttpPost]
        public IActionResult Index(DashboardViewModel dashboardViewModel,toDoList toDoList) 
        {
            dashboardViewModel.Orders = _appDbContext.Orders.Include(x => x.OrderItems).Include(x => x.Apartment.ApartmentCategory).Include(x => x.Apartment.ApartmentFeatures).Include(x => x.Apartment.ApartmentImages).Where(x => x.OrderStatus == 0).ToList();
            dashboardViewModel.ToDoLists = _appDbContext.ToDoListOrders.ToList();
            dashboardViewModel.AdminMessages = _appDbContext.AdminMessages.Take(4).ToList();
            dashboardViewModel.ToDoList = toDoList;
            toDoList.Work = dashboardViewModel.ToDoList.Work;
            toDoList.todiListDate=DateTime.Now;
            if (toDoList.Work is null)
            {
                ModelState.AddModelError("Work", "Cant be null");
                return View(dashboardViewModel);    
            }
            _appDbContext.Add(toDoList);
            _appDbContext.SaveChanges();
            return RedirectToAction("index"); 
        } 

        public IActionResult Delete(int id)
        {
            toDoList toDoList=_appDbContext.ToDoListOrders.FirstOrDefault(x => x.Id == id);
            if(toDoList == null) { return NotFound(); }
            _appDbContext.Remove(toDoList);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> CreateUser()
        {
            AppUser user = new AppUser
            {
                FullName = "Ceyhun Hacili",
                UserName = "Jekah"
               
            };
            await _userManager.CreateAsync(user,"Admin123");
            return Ok();
        }
        public async Task<IActionResult> CreateRole()
        {
            IdentityRole role1 = new IdentityRole("Admin");
            IdentityRole role2 = new IdentityRole("Member");

            await _roleManager.CreateAsync(role1);
            await _roleManager.CreateAsync(role2);

            return Ok("Role created");
        }

        public async Task<IActionResult> Setrole()
        {
            AppUser user = await _userManager.FindByNameAsync("Jekah");
            await _userManager.AddToRoleAsync(user, "Admin");
            return Ok("roles setted");
        }

        public IActionResult Test()
        {
            return View();
        }
    }
}
