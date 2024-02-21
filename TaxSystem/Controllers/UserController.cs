using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaxSystem.Contracts;
using TaxSystem.Data;
using TaxSystem.Models.Requests;
using TaxSystem.Models.User;
using TaxSystem.Services;

namespace TaxSystem.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private IRequestService requestService;

        public UserController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IRequestService _requestService
            )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            requestService = _requestService;

        }

        [AllowAnonymous]
        public IActionResult Login()
            => View(new LoginModel());

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                await signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }


        [AllowAnonymous]
        public IActionResult Register()
        {
            var model = new RegisterModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ApplicationUser user = new ApplicationUser()
            {
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "User");
                return RedirectToAction("Login", "User");
            }

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }

            return View(model);
        }

        public IActionResult RequestSer()
        {
            var model = new AddRequestViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RequestSer(AddRequestViewModel model)
        {
            var user = User.Identity as ApplicationUser;

            if (user == null || !ModelState.IsValid)
            {
                return View(new AddRequestViewModel());
            }

            model.UserId = user.Id;      
            
            await requestService.Add(model);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult MyServices()
        {
            return View();
        }
    }
}
