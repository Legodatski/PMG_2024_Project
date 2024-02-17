using Microsoft.AspNetCore.Identity;
using TaxSystem.Contracts;
using TaxSystem.Data;
using TaxSystem.Models.Requests;

namespace TaxSystem.Services
{
    public class RequestService : IRequestService
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> _userManager;

        public RequestService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            _userManager = userManager;
        }

        public async Task Add(AddRequestViewModel model)
        {
            var client = await context.Users.FindAsync(model.UserId);
            var service = await context.Services.FindAsync(model.ServiceId);
            var desksServices = context.DeskService.Where(x => x.ServiceId == model.ServiceId);
        }
    }
}
