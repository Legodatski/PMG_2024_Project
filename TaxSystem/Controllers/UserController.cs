﻿    using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaxSystem.Contracts;
using TaxSystem.Data;
using TaxSystem.Models.Requests;
using TaxSystem.Models.User;
using System.Web;
using Ganss.Xss;

namespace TaxSystem.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private IRequestService requestService;
        private IAmenityService amenityService;
        private HtmlSanitizer sanitizer;

        public UserController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IRequestService _requestService,
            IAmenityService _service
            )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            requestService = _requestService;
            amenityService = _service;
            sanitizer = new HtmlSanitizer();
        }

        [AllowAnonymous]
        public IActionResult Login()
            => View(new LoginModel());

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model)
        {
            string email = sanitizer.Sanitize(model.Email);
            string password = sanitizer.Sanitize(model.Password);

            if (!ModelState.IsValid ||
                email == null ||
                password == null)
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
            string username = sanitizer.Sanitize(model.Username);
            string firstName = sanitizer.Sanitize(model.FirstName);
            string lastName = sanitizer.Sanitize(model.LastName);
            string email = sanitizer.Sanitize(model.Email);
            string phoneNumber = sanitizer.Sanitize(model.PhoneNumber);

            if (!ModelState.IsValid ||
                username == null ||
                firstName == null ||
                lastName == null ||
                email == null ||
                phoneNumber == null)
            {
                return View();
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
            model.Services = amenityService.GetServiceNames();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RequestSer(AddRequestViewModel model)
        {
            var userName = HttpContext.User.Identity.Name;

            var User = await GetCurrentUser();

            if (User == null || !ModelState.IsValid)
            {
                throw new Exception();
            }

            model.User = User;

            string? result = await requestService.Add(model);

            if (result != null)
            {
                TempData["Error"] = result;
                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction("All");
        }

        public async Task<IActionResult> All(bool? completed)
        {
            var user = await GetCurrentUser();

            var model = requestService.GetUserRequests(user, completed);

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await requestService.Delete(id);

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        private async Task<ApplicationUser> GetCurrentUser()
        {
            var userName = HttpContext.User.Identity.Name;

            var User = await userManager.FindByNameAsync(userName);

            return User;
        }
    }
}
