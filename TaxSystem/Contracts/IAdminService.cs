using Microsoft.AspNetCore.Identity;
using TaxSystem.Data;
using TaxSystem.Models.User;

namespace TaxSystem.Contracts
{
    public interface IAdminService
    {
        public Task<IEnumerable<UserViewModel>> GetAllUsers(
            string? searchTerm = null,
            string? roleName = null,
            int currentPage = 1,
            int usersPerPage = 5);

        public Task<ApplicationUser> FindUser(string id);

        public Task EditUser(EditUserModel model);

        public Task<string> GetRoleNameByUserId(string id);

        public IEnumerable<IdentityRole> GetAllRoles();
    }
}
