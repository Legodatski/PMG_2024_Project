using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using TaxSystem.Contracts;
using TaxSystem.Data;
using TaxSystem.Models.Service;

namespace TaxSystem.Services
{
    public class ServiceService : IServiceService
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ServiceService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            _userManager = userManager;
        }

        public async Task Add(Service service)
        {
            await context.Services.AddAsync(service);
            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var service = await context.Services.FindAsync(id);

            if (service != null)
                service.IsDeleted = true;

            await context.SaveChangesAsync();
        }

        public async Task Edit(Service model)
        {
            var service = await context.Services.FindAsync(model.Id);

            if (service != null)
            {
                service.Name = model.Name;
                service.Description = model.Description;
                service.RequiredHours = model.RequiredHours;

                await context.SaveChangesAsync();
            }
        }

       public async Task<IEnumerable<ServiceViewModel>> GetAll(
            string? searchterm,
            int currentPage = 1,
            int usersPerPage = 5)
        {

             var services = context.Services.Where(x => x.IsDeleted == false).AsQueryable();
             var deskServ = context.DeskService.ToArray();
             var workers = await _userManager.GetUsersInRoleAsync("Worker");
             var desks = context.Desks.ToArray();

             var viewModels = new List<ServiceViewModel>();

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

                         var desk = desks.FirstOrDefault(x => x.Id == s.Id);
                         var worker = workers.FirstOrDefault(x => x.Id == desk.WorkerId);

                         toAdd.WorkerFirstNames.Add(worker.FirstName);
                         toAdd.WorkerLastNames.Add(worker.LastName);
                     }
                 }

                 viewModels.Add(toAdd);
             }

             if (searchterm != null)
             {
                 searchterm = searchterm.ToLower();

                 viewModels = viewModels.Where(x => x.Name.ToLower().Contains(searchterm) || x.Description.ToLower().Contains(searchterm)).ToList();
             }

             var result = viewModels.Skip((currentPage - 1) * usersPerPage).Take(usersPerPage).AsQueryable();

             return viewModels;
        }

        public async Task<Service> GetService(int id)
            => await context.Services.FindAsync(id);
    }
}
