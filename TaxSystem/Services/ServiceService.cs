using NuGet.Packaging;
using TaxSystem.Contracts;
using TaxSystem.Data;
using TaxSystem.Models.Service;

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

        public async Task<IEnumerable<ServiceViewModel>> GetAll(
            string? searchterm,  
            int currentPage = 1, 
            int usersPerPage = 5)
        {
            var services = context.Services.AsQueryable();
            var deskServ = context.DeskServices.ToArray();
            var workers = context.Workers

            List<ServiceViewModel> viewModels = new List<ServiceViewModel>();

            foreach (var s in services)
            {
                var toAdd = new ServiceViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    RequiredHours = s.RequiredHours
                };

                foreach (var d in deskServ)
                {
                    if (d.ServiceId == s.Id)
                    {
                        toAdd.DeskIds.Add(d.DeskId.ToString());
                    }
                }
            }

            if (searchterm != null)
            {
                services = services.Where(x => x.Name.ToLower().Contains(searchterm) || x.Description.ToLower().Contains(searchterm)).AsQueryable();
            }

            var result = services.Skip((currentPage - 1) * usersPerPage).Take(usersPerPage).AsQueryable();

            return result;
        }
    }
}
