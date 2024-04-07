using TaxSystem.Data;
using TaxSystem.Models.Requests;

namespace TaxSystem.Contracts
{
    public interface IRequestService
    {
        public Task<string> Add(AddRequestViewModel model);

        public IEnumerable<Request> GetUserRequests(ApplicationUser user, bool? completed = null);

        public Task Delete(int id);

        public Task Complete(int id);

        public IEnumerable<Request> GetWorkerRequests(int id, bool? completed = null);

        public bool CheckIfUserHasRequests(string userId);
    }
}
