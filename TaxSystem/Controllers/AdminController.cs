using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaxSystem.Contracts;
using TaxSystem.Data;
using TaxSystem.Models.Users;

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
            var queryResult = await _adminService.GetAllUsers(
                query.SearchTerm,
                query.RoleName,
                query.CurrentPage,
                query.UsersPerPage);

            query.Users = queryResult;

            return View(query);
        }
    }
}
