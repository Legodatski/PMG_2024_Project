using Microsoft.AspNetCore.Mvc;
using TaxSystem.Contracts;
using TaxSystem.Data;

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

        public IActionResult All()
        {
            return View();
        }


        public IActionResult Add()
        {
            Service service = new Service();

            return View(service);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Service input)
        {
            await _service.Add(input);

            return RedirectToAction(nameof(All));
        }
    }
}
