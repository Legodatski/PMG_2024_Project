using TaxSystem.Data;
using TaxSystem.Models.DeskModels;

namespace TaxSystem.Contracts
{
    public interface IDeskService
    {
        public Task Add(AddDeskViewModel input);

        public Task<IEnumerable<Desk>> GetAllDesks(string? searchTerm); 

        public Task<IEnumerable<ApplicationUser>> GetAllWorkersWithoutDesks();

        public Task AddDeskService(int deskId, string serviceName);

        public Task<Desk> GetDeskByWorkerId(string id);

        public Task<bool> IfDeskHasrequests(int deskId, string serviceName);
    }
}
