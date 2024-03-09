using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using TaxSystem.Contracts;
using TaxSystem.Data;
using TaxSystem.Models.DeskModels;

namespace TaxSystem.Services
{
    public class DeskService : IDeskService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        private const string workerRoleName = "Worker";

        public DeskService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task Add(AddDeskViewModel input)
        {
            Desk desk = new Desk()
            {
                WorkerId = input.WorkerId
            };


            await _context.Desks.AddAsync(desk);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllWorkersWithoutDesks()
        {
            var result = await _userManager.GetUsersInRoleAsync(workerRoleName);
            var workers = result.ToList();
            var workersWithoutDesks = new List<ApplicationUser>();

            foreach (var worker in workers)
            {
                if (!IfWorkerHasDesk(worker.Id))
                {
                    workersWithoutDesks.Add(worker);
                }
            }

            return workersWithoutDesks;
        }

        public async Task<IEnumerable<Desk>> GetAllDesks(
            string? searchTerm)
        {
            var allWorkers = await _userManager.GetUsersInRoleAsync(workerRoleName);
            var workers = allWorkers.ToList();

            var desks = _context.Desks.ToList().AsQueryable();

            if (searchTerm != null)
            {
                searchTerm = searchTerm.ToLower();

                desks = desks.Where(x =>
                x.Worker.FirstName.ToLower().Contains(searchTerm) ||
                x.Worker.LastName.ToLower().Contains(searchTerm))
                    .AsQueryable();
            }

            foreach (var d in desks)
            {
                d.Worker = workers.FirstOrDefault(x => x.Id == d.WorkerId);
            }

            return desks;
        }

        private bool IfWorkerHasDesk(string workerId)
        {
            var desks = _context.Desks.ToArray();

            foreach (var d in desks)
            {
                if (d.WorkerId == workerId)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task AddDeskService(int deskId, string serviceName)
        {
            var service = await _context.Services.FirstOrDefaultAsync(x => x.Name == serviceName);
            var desk = await _context.Desks.FirstOrDefaultAsync(x => x.Id == deskId);


            if (service != null)
            {
                DesksServices deskService = new DesksServices()
                {
                    DeskId = deskId,
                    ServiceId = service.Id,
                };

                if (!_context.DeskService.Contains(deskService))
                {
                    await _context.DeskService.AddAsync(deskService);
                    await _context.SaveChangesAsync();
                }
            }

        }

        public async Task<Desk> GetDeskByWorkerId(string id) 
            => await _context.Desks.FirstOrDefaultAsync(x => x.WorkerId == id);

        public async Task RemoveDesksSer(int deskId, string serName)
        {
            var ser = await _context.Services.FirstOrDefaultAsync(x => x.Name == serName);

            var toDel = await _context.DeskService.FirstOrDefaultAsync(x => x.DeskId == deskId && x.ServiceId == ser.Id);

            _context.DeskService.Remove(toDel);
            await _context.SaveChangesAsync();
        }
    }
}