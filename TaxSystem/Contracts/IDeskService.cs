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

        public Task RemoveDesksSer(int deskId, string serName);

        public Task Delete(int id);
    }
}
