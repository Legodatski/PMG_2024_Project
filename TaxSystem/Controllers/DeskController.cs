using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using TaxSystem.Contracts;
using TaxSystem.Data;
using TaxSystem.Models.DeskModels;
using Microsoft.AspNetCore.Http.Extensions;

namespace TaxSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DeskController : Controller
    {
        private IDeskService _deskService;
        private IAmenityService _service;

        public DeskController(IDeskService deskService, IAmenityService Amenity)
        {
            _service = Amenity;
            _deskService = deskService;
        }

        public async Task<IActionResult> All([FromQuery] AllDesksQueryModel query)
        {
            var queryResult = _deskService.GetAllDesks(query.SearchTerm);

            query.Desks = await queryResult;

            return View(query);
        }

        public async Task<IActionResult> Add()
        {
            AddDeskViewModel model = new AddDeskViewModel();

            var workers = await _deskService.GetAllWorkersWithoutDesks();

            model.AllWorkers = workers.ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddDeskViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _deskService.Add(model);

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> AddDeskService(int Id)
        {
            var model = new AddDeskServiceViewModel();
            model.DeskId = Id;
            model.AllServiceNames = await _service.GetServiceNamesExcludingDesk(Id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddDeskService(AddDeskServiceViewModel model)
        {
            var url = Request.GetDisplayUrl();
            var deskId = int.Parse(url.Last().ToString());
            model.DeskId = deskId;

            if (!ModelState.IsValid)
            {
                return View();
            }

            await _deskService.AddDeskService(model.DeskId, model.ServiceName);

            return RedirectToAction(nameof(All));
        }


        public async Task<IActionResult> RemoveDeskService(int id)
        {
            var model = new RemoveDeskServiceViewModel()
            {
                DeskId = id,
                ServicesNames = await _service.GetServiceNamesByDesk(id)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveDeskService(RemoveDeskServiceViewModel model)
        {
            var url = Request.GetDisplayUrl();
            var deskId = int.Parse(url.Last().ToString());
            model.DeskId = deskId;

            if (!ModelState.IsValid)
            {
                return View();
            }

            await _deskService.RemoveDesksSer(model.DeskId, model.ServiceName);

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _deskService.Delete(id);

            return RedirectToAction(nameof(All));
        }
    }
}
