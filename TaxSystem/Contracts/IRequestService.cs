using TaxSystem.Data;
using TaxSystem.Models.Requests;

namespace TaxSystem.Contracts
{
    public interface IRequestService
    {
        public Task Add(AddRequestViewModel model);

        public IEnumerable<Request> GetUserRequest(ApplicationUser user, bool? completed = null);
    }
}
