using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Controllers
{
    public class PassengersController : Controller
    {
        private readonly IPassengerService _service;

        public PassengersController(IPassengerService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View(_service.GetAll());
        }

        public async Task<IActionResult> Details(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result is null)
                return RedirectToAction("PageNotFound", "Home");
            return View(result);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PassengerModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            try
            {
                await _service.AddAsync(model);
                TempData[Constants.SuccessFieldName] = "Created successfully";
                return RedirectToAction(nameof(Details), new { model.Id });
            }
            catch (DbUpdateException exc)
            {
                TempData[Constants.ErrorFieldName] = exc.InnerException != null ? exc.InnerException.Message : exc.Message;
                return View(model);
            }
            catch (ArgumentException exc)
            {
                TempData[Constants.ErrorFieldName] = exc.Message;
                return View(model);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _service.GetByIdAsync(id);
            if (model is null)
                return RedirectToAction("PageNotFound", "Home");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PassengerModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            model.Id = id;
            try
            {
                await _service.UpdateAsync(model);
                TempData[Constants.SuccessFieldName] = "Edited successfully";
                return RedirectToAction(nameof(Details), new { model.Id });
            }
            catch (DbUpdateException exc)
            {
                TempData[Constants.ErrorFieldName] = exc.InnerException != null ? exc.InnerException.Message : exc.Message;
                return View(model);
            }
            catch (ArgumentException exc)
            {
                TempData[Constants.ErrorFieldName] = exc.Message;
                return View(model);
            }
            catch (InvalidOperationException)
            {
                return RedirectToAction("PageNotFound", "Home");
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _service.GetByIdAsync(id);
            if (model is null)
                return RedirectToAction("PageNotFound", "Home");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                await _service.DeleteByIdAsync(id);
                TempData[Constants.SuccessFieldName] = "Deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException exc)
            {
                TempData[Constants.ErrorFieldName] = exc.InnerException != null ? exc.InnerException.Message : exc.Message;
                var model = await _service.GetByIdAsync(id);
                if (model is null)
                    return RedirectToAction("PageNotFound", "Home");
                return View(model);
            }
            catch (InvalidOperationException)
            {
                return RedirectToAction("PageNotFound", "Home");
            }
        }
    }
}
