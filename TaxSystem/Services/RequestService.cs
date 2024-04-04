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

            desks = desks.OrderBy(x => x.Requests.Count());

            var desk = desks.First();

            var requests = context.Requests.Where(x => x.DeskId == desk.Id);

            TimeOnly time = new TimeOnly();

            if (requests.Any())
            {
                time = LastTime(requests.Select(x => x.Time).ToArray());
                var request = await requests.FirstOrDefaultAsync(x => x.Time == time.ToString());
                var ser = context.Services.FirstOrDefault(x => x.Id == request.AmenityId);


                time = time.AddMinutes(5);
                time = time.AddMinutes(double.Parse(ser.RequiredMinutes));


                if (time.Hour == 12)
                {
                    time = new TimeOnly(13, 0);
                }
                else if (time.Hour >= 17)
                {
                    throw new Exception("бюрото не работи");
                }
            }
            else
            {
                time = new TimeOnly(8, 0);
            }

            var newRequest = new Request()
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

            await context.Requests.AddAsync(newRequest);
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

            if (request != null)
            {
                var allrequests = context.Requests.Where(x => x.DeskId == request.DeskId).ToArray();
                var nextRequests = allrequests.Where(x => x.Id >= id).ToList();
                nextRequests = nextRequests.OrderBy(x => x.Id).ToList();
                List<Request> newR = new List<Request>();

                if (nextRequests != null)
                {
                    for (int i = 1; i < nextRequests.Count; i++)
                    {
                        newR.Add(new Request
                        {
                            Id = nextRequests[i].Id,
                            DeskId = nextRequests[i].DeskId,
                            AmenityId = nextRequests[i].AmenityId,
                            ClientId = nextRequests[i].ClientId,
                            IsCompleted = nextRequests[i].IsCompleted,
                            Time = nextRequests[i - 1].Time
                        });
                    }
                }

                context.RemoveRange(nextRequests);
                await context.AddRangeAsync(newR);

                await context.SaveChangesAsync();
            }

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

        private TimeOnly LastTime(string[] input)
        {
            TimeOnly mTime = new TimeOnly(8,0);

            foreach (var c in input)
            {
                TimeOnly curTime = TimeOnly.Parse(c);

                if (mTime <= curTime)
                {
                    mTime = curTime;
                }
            }

            return mTime;
        }
    }
}
