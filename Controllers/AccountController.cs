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

        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);
            //check email
            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
            //email is already used
            if (user != null)
            {
                TempData["Error"] = "Email is already used. Try another email";
                return View(registerVM);
            }
            //register
            var newUser = new AppUser()
            {
                Email = registerVM.EmailAddress,
                UserName = registerVM.EmailAddress
            };

            var registerResponse = await _userManager.CreateAsync(newUser, registerVM.Password);

            if (registerResponse.Succeeded)
            {
                // Assign role
                await _userManager.AddToRoleAsync(newUser, "User");
                return RedirectToAction("Login", "Account");
            }
            else
            {
                // Display errors if registration fails
                foreach (var error in registerResponse.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                TempData["Error"] = "Registration failed. Please check the errors.";
                return View(registerVM);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
