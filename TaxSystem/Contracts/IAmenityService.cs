using TaxSystem.Data;
using TaxSystem.Models.Amenity;

namespace TaxSystem.Contracts
{
    public interface IAmenityService
    {
        public Task Add(Amenity Amenity);

        public Task<IEnumerable<AmenityViewModel>> GetAll(
            string? searchterm);

        public Task Edit(Amenity model);

        public IEnumerable<string> GetServiceNames();

        public Task<Amenity> GetService(int id);

        public Task Delete(int id);

        public Task<IEnumerable<string>> GetServiceNamesExcludingDesk(int deskId);

        public Task<IEnumerable<string>> GetServiceNamesByDesk(int deskId);
    }
}
