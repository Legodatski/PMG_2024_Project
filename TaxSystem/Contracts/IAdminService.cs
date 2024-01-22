using TaxSystem.Data;

namespace TaxSystem.Contracts
{
    public interface IAdminService
    {
        public Task<IEnumerable<ApplicationUser>> GetAllUsers(
            string? searchTerm = null,
            string? roleName = null,
            int currentPage = 1,
            int usersPerPage = 5);
    }
}
