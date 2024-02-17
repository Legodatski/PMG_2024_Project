using TaxSystem.Contracts;
using TaxSystem.Data;

namespace TaxSystem.Services
{
    public class DeskService : IDeskService
    {
        private readonly ApplicationDbContext _context;

        public DeskService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Desk input)
        {
            await _context.Desks.AddAsync(input);
            await _context.SaveChangesAsync();
        }
    }
}