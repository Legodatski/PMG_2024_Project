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

        public AdminController(
            IAdminService adminService
            )
        {
            _adminService = adminService;
        }

        public async Task<IActionResult> AllUsers([FromQuery] AllUsersQueryModel query)
        {
            var queryResult = _adminService.GetAllUsers(
                query.SearchTerm,
                query.RoleName,
                query.CurrentPage,
                query.UsersPerPage);

            query.Users = await queryResult;

            return View(query);
        }

        public async Task<IActionResult> Edit(string id)
        {
            ApplicationUser user = await _adminService.FindUser(id);
            //to add if the user has a serviced issued

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
            await _adminService.EditUser(model);

            return RedirectToAction(nameof(AllUsers));
        }
    }
}
