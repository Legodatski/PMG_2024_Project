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
    }
}
