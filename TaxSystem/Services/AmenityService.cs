using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using TaxSystem.Contracts;
using TaxSystem.Data;
using TaxSystem.Models.Amenity;

namespace TaxSystem.Services
{
    public class AmenityService : IAmenityService
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AmenityService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            _userManager = userManager;
        }

        public async Task Add(Amenity Amenity)
        {
            await context.Services.AddAsync(Amenity);
            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var Amenity = await context.Services.FindAsync(id);

            if (Amenity != null)
                context.Services.Remove(Amenity);

            await context.SaveChangesAsync();
        }

        public async Task Edit(Amenity model)
        {
            var Amenity = await context.Services.FindAsync(model.Id);

            if (Amenity != null)
            {
                Amenity.Name = model.Name;
                Amenity.Description = model.Description;
                Amenity.RequiredMinutes = model.RequiredMinutes;

                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<AmenityViewModel>> GetAll(
             string? searchterm)
        {

            var services = context.Services.AsQueryable();
            var deskServ = context.DeskService.ToArray();
            var workers = await _userManager.GetUsersInRoleAsync("Worker");
            var desks = context.Desks.ToArray();

            var viewModels = new List<AmenityViewModel>();

            foreach (var s in services)
            {
                var toAdd = new AmenityViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    RequiredMinutes = s.RequiredMinutes,
                };

                var desksIds = s.Desks.Select(x => x.DeskId.ToString()).ToList();
                toAdd.DeskIds = desksIds;
                
                var workerFirstNames = s.Desks.Select(x => x.Desk.Worker.FirstName).ToList();
                toAdd.WorkerFirstNames = workerFirstNames;

                var workerLastNames = s.Desks.Select(x => x.Desk.Worker.LastName).ToList();
                toAdd.WorkerLastNames = workerLastNames;

                viewModels.Add(toAdd);
            }

            if (searchterm != null)
            {
                searchterm = searchterm.ToLower();

                viewModels = viewModels.Where(x => x.Name.ToLower().Contains(searchterm) || x.Description.ToLower().Contains(searchterm)).ToList();
            }

            return viewModels;
        }

        public async Task<Amenity> GetService(int id)
            => await context.Services.FindAsync(id);

        public IEnumerable<string> GetServiceNames()
        {
            var services = context.Services.ToArray();
            List<string> names = new List<string>();

            foreach (var Amenity in services)
            {
                names.Add(Amenity.Name);
            }

            return names;
        }

        public async Task<IEnumerable<string>> GetServiceNamesByDesk(int deskId)
        {
            var requests = context.Requests.Where(x => x.DeskId == deskId);

            var exclNames = await context.Services
                .Where(x => x.Desks
                .Select(a => a.DeskId).Contains(deskId) && requests.Select(b => b.AmenityId).Contains(x.Id))
                .Select(y => y.Name)
                .ToListAsync();

            var names = await context.Services
                .Where(x => x.Desks
                .Select(a => a.DeskId).Contains(deskId))
                .Select(y => y.Name)
                .ToListAsync();


            return names.Except(exclNames);
        }

        public async Task<IEnumerable<string>> GetServiceNamesExcludingDesk(int deskId)
        {
            var allServices = await context.Services.ToArrayAsync();
            var servicesInDesk = await context.Services.Where(x => x.Desks.Select(a => a.DeskId).Contains(deskId)).ToArrayAsync();

            var names = allServices.Except(servicesInDesk).Select(x => x.Name);

            return names;
        }
    }
}
