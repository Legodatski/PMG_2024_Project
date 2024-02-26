using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaxSystem.Contracts;
using TaxSystem.Data;

namespace TaxSystem.Controllers
{
    [Authorize(Roles="Worker")]
    public class WorkerController : Controller
    {
        private readonly IDeskService deskService;
        private readonly IRequestService requestService;
        private readonly UserManager<ApplicationUser> userManager;

        public WorkerController(IDeskService deskService, IRequestService requestService, UserManager<ApplicationUser> userManager)
        {
            this.deskService = deskService;
            this.requestService = requestService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> RequestsByWorker(bool? completed)
        {
            var userName = HttpContext.User.Identity.Name;
            var User = await userManager.FindByNameAsync(userName);

            var desk = await deskService.GetDeskByWorkerId(User.Id);

            var model = requestService.GetWorkerRequests(desk.Id, completed);

            return View(model);
        }

        public async Task<IActionResult> Complete(int id)
        {
            await requestService.Complete(id);

            return RedirectToAction(nameof(RequestsByWorker), true);
        }
    }
}
