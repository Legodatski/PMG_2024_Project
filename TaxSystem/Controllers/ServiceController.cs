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
            var queryResult = _service.GetAll(
                query.SearchTerm,
                query.CurrentPage,
                query.UsersPerPage);

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
            await _service.Add(input);

            return RedirectToAction(nameof(All));
        }
    }
}
