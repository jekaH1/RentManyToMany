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
                Orders = _appDbContext.Orders.Include(x => x.OrderItems).Include(x => x.Apartment.ApartmentCategory).Include(x => x.Apartment.ApartmentFeatures).Include(x => x.Apartment.ApartmentImages).Where(x=>x.OrderStatus ==0).ToList(),
            };

            return View(dashboardViewModel);
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
