using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaxSystem.Contracts;
using TaxSystem.Data;

namespace TaxSystem.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext context;

        public AdminService(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            this.context = context;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsers(string? searchTerm = null, 
            string? roleName = null,
            int currentPage = 1,
            int usersPerPage = 5)
        {
            IEnumerable<ApplicationUser> usersQuery = context.Users.Where(x => x.IsDeleted == false).AsQueryable();

            if (roleName != null)
            {
                var usersWithRole = _userManager.GetUsersInRoleAsync(roleName).GetAwaiter().GetResult().ToList();

                usersQuery = usersWithRole.Where(x => x.IsDeleted == false).ToList();
            }

            if (searchTerm != null)
            {
                string lowerTerm = searchTerm.ToLower();

                usersQuery = usersQuery.Where(x => 
                x.UserName.ToLower().Contains(lowerTerm) ||
                x.FirstName.ToLower().Contains(lowerTerm) ||
                x.LastName.ToLower().Contains(lowerTerm));
            }

            var users = usersQuery.Skip((currentPage - 1) * usersPerPage).Take(usersPerPage);

            return users;
        }
    }
}
