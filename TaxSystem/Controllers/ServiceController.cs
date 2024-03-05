using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaxSystem.Contracts;
using TaxSystem.Data;
using TaxSystem.Models.Service;

namespace TaxSystem.Controllers
{
    public class ServiceController : Controller
    {

        private readonly IServiceService _service;

        public ServiceController(
            IServiceService service)
        {
            _service = service;
        }

        public async Task<IActionResult> All([FromQuery] AllServicesQueryModel query)
        {
            var queryResult = _service.GetAll(query.SearchTerm);

            query.Services = await queryResult;

            return View(query);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            Service service = new Service();

            return View(service);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(Service input)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _service.Add(input);

            return RedirectToAction(nameof(All));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _service.GetService(id);

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(Service input)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _service.Edit(input);

            return RedirectToAction(nameof(All));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);

            return RedirectToAction(nameof(All));
        }
    }
}
