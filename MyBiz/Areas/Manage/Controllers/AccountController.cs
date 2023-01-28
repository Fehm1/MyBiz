using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBiz.Areas.Manage.ViewModels;
using MyBiz.Models;

namespace MyBiz.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AdminLoginViewModel adminLoginVM)
        {
            AppUser admin = await _userManager.FindByNameAsync(adminLoginVM.UserName);
            if (admin == null)
            {
                ModelState.AddModelError("", "Username or password is invalid!");
                return View(adminLoginVM);
            }

            var result = await _signInManager.PasswordSignInAsync(admin, adminLoginVM.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password is invalid!");
                return View(adminLoginVM);
            }

            return RedirectToAction("Index","dashboard");
        }

        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();

            return RedirectToAction("index", "home", new { area = "" });
        }
    }
}
