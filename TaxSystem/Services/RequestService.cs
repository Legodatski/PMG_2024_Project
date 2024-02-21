using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaxSystem.Contracts;
using TaxSystem.Data;
using TaxSystem.Models.Requests;

namespace TaxSystem.Services
{
    public class RequestService : IRequestService
    {
        private readonly ApplicationDbContext context;

        public RequestService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task Add(AddRequestViewModel model)
        {
            var client = await context.Users.FindAsync(model.UserId);
            var service = await context.Services.FirstOrDefaultAsync(x => x.Name == model.ServiceName);
            var desks = context.DeskService.Where(x => x.Service == service).Select(y => y.Desk);

            desks = desks.OrderBy(x => x.Services.Count());

            var desk = desks.First();

            var request = new Request()
            {
                Desk = desk,
                DeskId = desk.Id,
                Client = client,
                ClientId = client.Id,
                Service = service,
                ServiceId = service.Id,
                IsDeleted = false
            };

            await context.Requests.AddAsync(request);
            await context.SaveChangesAsync();
        }
    }
}
