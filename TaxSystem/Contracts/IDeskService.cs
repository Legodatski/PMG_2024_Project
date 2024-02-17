using TaxSystem.Data;

namespace TaxSystem.Contracts
{
    public interface IDeskService
    {
        public Task Add(Desk input);
    }
}
