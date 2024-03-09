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
            var Amenity = await context.Services.FirstOrDefaultAsync(x => x.Name == model.ServiceName);
            var desks = context.DeskService.Where(x => x.Amenity == Amenity).Select(y => y.Desk);

            desks = desks.OrderBy(x => x.Amenities.Count());

            var desk = desks.First();

            var requests = context.Requests.Where(x => x.DeskId == desk.Id);

            TimeOnly time = new TimeOnly();

            if (requests.Any())
            {
                requests.OrderByDescending(x => StringToTime(x.Time));

                string tInput = requests.First().Time;

                time = StringToTime(tInput);

                time = time.AddMinutes(double.Parse(Amenity.RequiredMinutes));
                time = time.AddMinutes(5);

                if (time.Hour == 12)
                {
                    time = new TimeOnly(13, 0);
                }
                else if (time.Hour >= 17)
                {
                    throw new Exception("not gud time");
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
                Amenity = Amenity,
                AmenityId = Amenity.Id,
                IsCompleted = false,
                Time = time.ToString(),
            };

            await context.Requests.AddAsync(request);
            await context.SaveChangesAsync();
        }

        public bool CheckIfUserHasRequests(string userId)
            => context.Requests.Any(x => x.Client.Id == userId);
        public async Task Complete(int id)
        {
            var request = await context.Requests.FirstOrDefaultAsync(x => x.Id == id);
            request.IsCompleted = true;
            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var services = await context.Services.ToArrayAsync();
            var request = await context.Requests.FirstOrDefaultAsync(x => x.Id == id);
            var allrequests = context.Requests.Where(x => x.DeskId == request.DeskId).ToArray();

            var delTime = StringToTime(request.Time);
            var serTime = double.Parse(request.Amenity.RequiredMinutes);

            if (request != null)
                context.Requests.Remove(request);

            foreach (var r in allrequests)
            {
                var time = StringToTime(r.Time);

                if (time > delTime)
                {
                    time = time.AddMinutes(-serTime);
                    r.Time = time.ToString();
                }
            }

            await context.SaveChangesAsync();
        }

        public IEnumerable<Request> GetUserRequests(
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
            else if (completed == true)
            {
                requests = requests.Where(x => x.IsCompleted == true).ToArray();
            }

            return requests;
        }

        public IEnumerable<Request> GetWorkerRequests(int id, bool? completed = null)
        {
            var services = context.Services.ToArray();
            var clients = context.Users.ToArray();
            var requests = context.Requests.Where(x => x.DeskId == id).ToArray();

            if (completed == false)
            {
                requests = requests.Where(x => x.IsCompleted == false).ToArray();
            }
            else if (completed == true)
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
