using Microsoft.AspNetCore.Mvc;
using TaxSystem.Contracts;
using TaxSystem.Data;

namespace TaxSystem.Controllers
{
    public class DeskController : Controller
    {
        private IDeskService deskService;

        public DeskController(IDeskService _deskService)
        {
            deskService = _deskService;
        }

        public async Task<IActionResult> All()
        {
            return View();
        }

        public IActionResult Add()
        {
            Desk model = new Desk();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Desk model)
        {
            await deskService.Add(model);

            return RedirectToAction(nameof(Add));
        }
    }
}
