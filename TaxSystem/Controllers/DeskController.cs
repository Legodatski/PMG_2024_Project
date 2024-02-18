using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaxSystem.Contracts;
using TaxSystem.Data;
using TaxSystem.Models.DeskModels;

namespace TaxSystem.Controllers
{
    public class DeskController : Controller
    {
        private IDeskService _deskService;

        public DeskController(IDeskService deskService)
        {
            _deskService = deskService;
        }

        public async Task<IActionResult> All([FromQuery] AllDesksQueryModel query)
        {
            var queryResult = _deskService.GetAllDesks(
                query.SearchTerm,
                query.CurrentPage, 
                query.DesksPerPage);

            query.Desks = await queryResult;

            return View(query);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add()
        {
            AddDeskViewModel model = new AddDeskViewModel();

            var workers = await _deskService.GetAllWorkersWithoutDesks();

            model.AllWorkers = workers.ToList();

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(AddDeskViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _deskService.Add(model);

            return RedirectToAction(nameof(All));
        }
    }
}
