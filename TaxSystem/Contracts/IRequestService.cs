using TaxSystem.Models.Requests;

namespace TaxSystem.Contracts
{
    public interface IRequestService
    {
        public Task Add(AddRequestViewModel model);
    }
}
