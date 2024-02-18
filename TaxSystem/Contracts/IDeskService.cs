using TaxSystem.Data;
using TaxSystem.Models.DeskModels;

namespace TaxSystem.Contracts
{
    public interface IDeskService
    {
        public Task Add(AddDeskViewModel input);

        public Task<IEnumerable<Desk>> GetAllDesks(
            string? searchTerm, 
            int currentPage, 
            int desksPerPage); 

        public Task<IEnumerable<ApplicationUser>> GetAllWorkersWithoutDesks();
    }
}
