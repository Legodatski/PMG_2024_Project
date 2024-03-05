using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaxSystem.Contracts;
using TaxSystem.Data;
using TaxSystem.Models.User;

namespace TaxSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IRequestService _requestService;

        public AdminController(
            IAdminService adminService,
            IRequestService requestService
            )
        {
            _requestService = requestService;
            _adminService = adminService;
        }

        public async Task<IActionResult> AllUsers([FromQuery] AllUsersQueryModel query)
        {
            var queryResult = _adminService.GetAllUsers(
                query.SearchTerm,
                query.RoleName);

            query.Users = await queryResult;

            return View(query);
        }

        public async Task<IActionResult> Edit(string id)
        {
            ApplicationUser user = await _adminService.FindUser(id);

            if (_requestService.CheckIfUserHasRequests(user.Id))
            {
                return RedirectToAction(nameof(AllUsers));
            }

            EditUserModel model = new EditUserModel
            {
                Id = id,
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = _adminService.GetAllRoles(),
                RoleName = await _adminService.GetRoleNameByUserId(user.Id)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _adminService.EditUser(model);

            return RedirectToAction(nameof(AllUsers));
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _adminService.DeleteUser(id);
            //to add if the user has a serviced issued

            return RedirectToAction(nameof(AllUsers));
        }
    }
}
