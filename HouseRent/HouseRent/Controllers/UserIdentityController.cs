using HouseRent.Context;
using HouseRent.Models;
using HouseRent.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HouseRent.Controllers
{
    public class UserIdentityController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public UserIdentityController(AppDbContext appDbContext,SignInManager<AppUser> signInManager,UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel userLoginVM)
        {
            if (!ModelState.IsValid) { return View(userLoginVM); }
            AppUser user=null;
            user = await _userManager.FindByNameAsync(userLoginVM.Username);
            if (user is null)
            {
                ModelState.AddModelError("", "Invalid Username or Password");
                return View(user);
            }
            var result = await _signInManager.PasswordSignInAsync(user, userLoginVM.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid Username or Password");
                return View(user);
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterViewModel userRegisterVM)
        {
            if(!ModelState.IsValid) { return View(userRegisterVM); }
            AppUser user = null;

            user = await _userManager.FindByNameAsync(userRegisterVM.Username);
            if (user != null)
            {
                ModelState.AddModelError("Username", "Username already exsists");
                return View(userRegisterVM);
            }
            user = await _userManager.FindByEmailAsync(userRegisterVM.Email);
            if (user != null)
            {
                ModelState.AddModelError("Username", "Email already exsists");
                return View(userRegisterVM);
            }

            user = new AppUser
            {
                UserName = userRegisterVM.Username,
                Email = userRegisterVM.Email,
                FullName = userRegisterVM.Fullname,
                PhoneNumber=userRegisterVM.PhoneNumber
            };

            await _userManager.CreateAsync(user, userRegisterVM.Password);
            await _userManager.AddToRoleAsync(user, "Member");
            return RedirectToAction("Index","home");
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","home");
        }

        
    }
}
