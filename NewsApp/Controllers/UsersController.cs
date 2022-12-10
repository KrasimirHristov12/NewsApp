using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsApp.Data;
using NewsApp.Data.Models;
using NewsApp.Models.Users;
using NewsApp.Services.Emails;

namespace NewsApp.Controllers
{
    public class UsersController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IEmailSenderService emailSender;
        private readonly IRepository repository;

        public UsersController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IEmailSenderService emailSender, IRepository repository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
            this.repository = repository;
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterInputModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                EmailConfirmed = false

            };
            var createResult = await userManager.CreateAsync(user, model.Password);
            if (!createResult.Succeeded)
            {
                foreach (var err in createResult.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
                return View(model);
            }
            await emailSender.SendEmailAsync(user.Email, user.UserName, user.Id);
            return RedirectToAction(nameof(Login));

        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginInputModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var foundUser = await userManager.FindByNameAsync(model.UserName);
            if (foundUser == null)
            {
                ModelState.AddModelError("", "The username or password you typed is incorrect!");
                return View(model);
            }

            var signInResult = await signInManager.PasswordSignInAsync(foundUser, model.Password, false, false);

            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "The username or password you typed is incorrect!");
                return View(model);
            }

            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> Logout(string id)
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        
        [AllowAnonymous]

        public async Task<IActionResult> Confirm(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.EmailConfirmed = true;
                await repository.UpdateAsync<ApplicationUser>(user);

                return Content("Your account is confirmed!");
            }
                return NotFound();
            
        }
    }
}
