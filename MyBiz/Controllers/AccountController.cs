using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBiz.Models;
using MyBiz.ViewModels;

namespace MyBiz.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(MemberRegisterViewModel memberRegisterVM)
        {
            AppUser member = await _userManager.FindByNameAsync(memberRegisterVM.UserName);

            if (member != null)
            {
                ModelState.AddModelError("UserName", "Username exist!");
                return View(member);
            }

            member = await _userManager.FindByEmailAsync(memberRegisterVM.Email);

            if (member != null)
            {
                ModelState.AddModelError("Email", "Email exist!");
                return View(member);
            }

            member = new AppUser
            {
                UserName = memberRegisterVM.UserName,
                FullName = memberRegisterVM.FullName,
                Email = memberRegisterVM.Email,
                PhoneNumber = memberRegisterVM.Phone
            };

            var result = await _userManager.CreateAsync(member, memberRegisterVM.Password);
            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                    return View(member);
                }
            }

            await _userManager.AddToRoleAsync(member, "Member");

            return RedirectToAction("login", "account");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(MemberLoginViewModel memberLoginVM)
        {
            AppUser member = await _userManager.FindByNameAsync(memberLoginVM.UserName);
            if (member == null)
            {
                ModelState.AddModelError("", "Username or password is invalid!");
                return View(memberLoginVM);
            }

            var result = await _signInManager.PasswordSignInAsync(member, memberLoginVM.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password is invalid!");
                return View(memberLoginVM);
            }

            return RedirectToAction("index", "home");
        }

        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();

            return RedirectToAction("index", "home");
        }
    }
}
