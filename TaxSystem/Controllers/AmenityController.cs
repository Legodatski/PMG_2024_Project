using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaxSystem.Contracts;
using TaxSystem.Data;
using TaxSystem.Models.Amenity;

namespace TaxSystem.Controllers
{
    public class AmenityController : Controller
    {

        private readonly IAmenityService _service;

        public AmenityController(
            IAmenityService Amenity)
        {
            _service = Amenity;
        }

        public async Task<IActionResult> All([FromQuery] AllAmenitiesQueryModel query)
        {
            var queryResult = _service.GetAll(query.SearchTerm);

            query.Services = await queryResult;

            return View(query);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            Amenity Amenity = new Amenity();

            return View(Amenity);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(Amenity input)
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
        public async Task<IActionResult> Edit(Amenity input)
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
