using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RunGroup.Data;
using RunGroup.Models;
using RunGroup.ViewModels;

namespace RunGroup.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            
        }

        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if(!ModelState.IsValid) return View(login);

            var user = await _userManager.FindByEmailAsync(login.EmailAddress);

            if(user != null)
            {
                //user is found
                var passwordCheck = await _userManager.CheckPasswordAsync(user,login.Password);
                if (passwordCheck)
                {
                    //correct password sign in
                    var result = await _signInManager.PasswordSignInAsync(user, login.Password, false, false);
                    if(result.Succeeded)
                    {
                        return RedirectToAction("Index","Races");
                    }
                }
                //password is incorrect
                TempData["Error"] = "Wrong credentials. Please Try again";
                return View(login);
            }
            //user not found
            TempData["Error"] = "Wrong credentials. Please Try again";
            return View(login);
        }

        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost] 
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid) return View(register);

            var user = await _userManager.FindByEmailAsync(register.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(register);
            }

            var newUser = new AppUser()
            {
                Email = register.EmailAddress,
                UserName = register.EmailAddress,
            };

            var newUserResponce = await _userManager.CreateAsync(newUser, register.Password);
            if (newUserResponce.Succeeded) 
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                
            return RedirectToAction("Index","Races");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Races");
        }
    }
}
