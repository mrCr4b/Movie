using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Movie.Models.ViewModels;
using Movie.Models;

namespace Movie.Controllers
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

        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
                return View(loginVM);

            // check email first
            var user = await _userManager.FindByEmailAsync(loginVM.Email);
            // wrong user
            if (user == null)
            {
                TempData["Error"] = "Wrong credentials. Try again";
                return View(loginVM);
            }

            // check pass
            var pass = await _userManager.CheckPasswordAsync(user, loginVM.Password);
            if (pass)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                // login
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
            }
            // wrong pass
            TempData["Error"] = "Wrong credentials. Try again";
            return View(loginVM);
        }
    }
}
