using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaxSystem.Contracts;
using TaxSystem.Data;
using TaxSystem.Models.User;

namespace TaxSystem.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext context;
        private const string adminId = "35bbae7d-b2a0-472a-8137-e8df5f4ac614";
        private const string adminRoleId = "3d7c794e-daf1-422c-b5d6-ea9934ed597d";

        public AdminService(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            this.context = context;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllUsers(string? searchTerm = null, 
            string? roleName = null,
            int currentPage = 1,
            int usersPerPage = 5)
        {
            var users = context.Users.Where(x => x.IsDeleted == false && x.Id != adminId).AsQueryable();
            var roles = context.Roles.ToArray();
            var userRoles = context.UserRoles.ToArray();

            List<UserViewModel> usersQuery = new List<UserViewModel>();

            foreach (var item in users)
            {
                var curUserRole = userRoles.FirstOrDefault(x => x.UserId == item.Id);
                var curRole = roles.FirstOrDefault(x => x.Id == curUserRole.RoleId);

                usersQuery.Add(new UserViewModel
                {
                    Id = item.Id,
                    UserName = item.UserName,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Email = item.Email,
                    PhoneNumber = item.PhoneNumber,
                    RoleName = curRole.Name
                });
            }

            if (roleName != null)
            {
                usersQuery = usersQuery.Where(x => x.RoleName == roleName).ToList();
            }

            if (searchTerm != null)
            {
                string lowerTerm = searchTerm.ToLower();

                usersQuery = usersQuery.Where(x => 
                x.UserName.ToLower().Contains(lowerTerm) ||
                x.FirstName.ToLower().Contains(lowerTerm) ||
                x.LastName.ToLower().Contains(lowerTerm)).ToList();
            }

            var usersResult = usersQuery.Skip((currentPage - 1) * usersPerPage).Take(usersPerPage).AsQueryable();

            return usersResult;
        }

        public async Task<ApplicationUser> FindUser(string id)
            => await context.Users.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);

        public async Task<string> GetRoleNameByUserId(string id)
        {
            var user = await FindUser(id);

            var roles = await _userManager.GetRolesAsync(user);

            return roles.First();
        }

        public IEnumerable<IdentityRole> GetAllRoles()
            => context.Roles.Where(x => x.Id != adminRoleId);

        public async Task EditUser(EditUserModel model)
        {
            var user = await FindUser(model.Id);

            user.UserName = model.Username;
            user.FirstName = model.FirstName;
            user.LastName = user.LastName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;

            var toRemove = context.UserRoles.Where(x => x.UserId == model.Id).ToList();

            context.UserRoles.RemoveRange(toRemove);

            string roleId = context.Roles.FirstOrDefault(x => x.Name == model.RoleName).Id;

            await context.UserRoles.AddAsync(new IdentityUserRole<string>
            {
                UserId = model.Id,
                RoleId = roleId
            });

            await context.SaveChangesAsync();
        }

        public async Task DeleteUser(string id)
        {
            var user = await context.Users.FindAsync(id);

            user.IsDeleted = true;

            await context.SaveChangesAsync();
        }
    }
}
