using HouseRent.Models;
using HouseRent.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HouseRent.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AdminLoginController : Controller
    {
        private readonly UserManager<AppUser> _Usermanager;
        private readonly SignInManager<AppUser> _SignInManager;

        public AdminLoginController(UserManager<AppUser> usermanager, SignInManager<AppUser> signInManager)
        {
            _Usermanager = usermanager;
            _SignInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginViewModel adminLoginVM)
        {
            if (!ModelState.IsValid) return View(adminLoginVM);
            AppUser user = null;
            user = await _Usermanager.FindByNameAsync(adminLoginVM.UserName);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username or pass");
                return View(adminLoginVM);
            }
            var result = await _SignInManager.PasswordSignInAsync(user, adminLoginVM.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid username or pass");
                return View(adminLoginVM);
            }
            return RedirectToAction("index", "dashboard");
        }

        public async Task<IActionResult> Logout()
        {
            await _SignInManager.SignOutAsync();
            return RedirectToAction("login");
        }
    }
}
