using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.ComponentModel.Design.Serialization;
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
            var service = await context.Services.FirstOrDefaultAsync(x => x.Name == model.ServiceName);
            var desks = context.DeskService.Where(x => x.Service == service).Select(y => y.Desk);

            desks = desks.OrderBy(x => x.Services.Count());

            var desk = desks.First();

            var requests = context.Requests.Where(x => x.DeskId == desk.Id);

            TimeOnly time = new TimeOnly();

            if (requests.Any())
            {
                requests.OrderByDescending(x => StringToTime(x.Time));

                string tInput = requests.First().Time;

                time = StringToTime(tInput);

                time = time.AddMinutes(double.Parse(service.RequiredMinutes));
                time = time.AddMinutes(5);

                if (time.Hour == 12)
                {
                    time = new TimeOnly(13, 0);
                }
                else if (time.Hour >= 17)
                {
                    //throw exception
                }
            }
            else
            {
                time = new TimeOnly(8, 0);
            }

            var request = new Request()
            {
                Desk = desk,
                DeskId = desk.Id,
                Client = model.User,
                ClientId = model.User.Id,
                Service = service,
                ServiceId = service.Id,
                IsDeleted = false,
                IsCompleted = false,
                Time = time.ToString(),
            };

            await context.Requests.AddAsync(request);
            await context.SaveChangesAsync();
        }

        public IEnumerable<Request> GetUserRequest(
            ApplicationUser user, 
            bool? completed = null)
        {
            var services = context.Services.ToArray();
            var desks = context.Desks.ToArray();
            var requests = context.Requests.Where(x => x.ClientId == user.Id).ToArray();


            if (completed == false)
            {
                requests = requests.Where(x => x.IsCompleted == false).ToArray();
            }
            else if(completed == true)
            {
                requests = requests.Where(x => x.IsCompleted == true).ToArray();
            }

            return requests;
        }

        private TimeOnly StringToTime(string str)
        {
            var input = str.Split(new char[] { ',', ' ', ':', '.' }, StringSplitOptions.RemoveEmptyEntries);

            var time = new TimeOnly(int.Parse(input[0]), int.Parse(input[1]));

            return time;
        }
    }
}
