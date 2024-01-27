using TaxSystem.Contracts;
using TaxSystem.Data;

namespace TaxSystem.Services
{
    public class ServiceService : IServiceService
    {
        private readonly ApplicationDbContext context;

        public ServiceService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task Add(Service service)
        {
            await context.Services.AddAsync(service);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Service>> GetAll(
            string? searchterm, 
            string? roleName = null, 
            int currentPage = 1, 
            int usersPerPage = 5)
        {
            var services = context.Services.AsQueryable();

            if (searchterm != null)
            {
                services = services.Where(x => x.Name.ToLower().Contains(searchterm) || x.Description.ToLower().Contains(searchterm)).AsQueryable();
            }

            var result = services.Skip((currentPage - 1) * usersPerPage).Take(usersPerPage).AsQueryable();

            return result;
        }
    }
}
