using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class VisasController : Controller
    {
        private readonly IVisaService _service;

        private readonly IPassengerService _passengerService;

        private readonly IMapper _mapper;

        public VisasController(IVisaService service, IPassengerService passengerService, IMapper mapper)
        {
            _service = service;
            _passengerService = passengerService;
            _mapper = mapper;
        }

        // GET: VisasController
        public IActionResult Index()
        {
            return View(_service.GetAll());
        }

        // GET: VisasController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result is null)
                return RedirectToAction("PageNotFound", "Home");
            return View(result);
        }

        // GET: VisasController/Create
        public ActionResult Create()
        {
            SetPassengers();
            return View();
        }

        private void SetPassengers()
        {
            ViewBag.Passengers = _mapper.Map<IEnumerable<PassengerReduced>>(_passengerService.GetAll());
        }

        // POST: VisasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VisaModel model)
        {
            if (!ModelState.IsValid)
            {
                SetPassengers();
                return View(model);
            }
            try
            {
                await _service.AddAsync(model);
                TempData[Constants.SuccessFieldName] = "Created successfully";
                return RedirectToAction(nameof(Details), new {model.Id});
            }
            catch (DbUpdateException exc)
            {
                SetPassengers();
                TempData[Constants.ErrorFieldName] = exc.InnerException != null ? exc.InnerException.Message : exc.Message;
                return View(model);
            }
            catch (ArgumentException exc)
            {
                SetPassengers();
                TempData[Constants.ErrorFieldName] = exc.Message;
                return View(model);
            }
        }

        // GET: VisasController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _service.GetByIdAsync(id);
            if (model is null)
                return RedirectToAction("PageNotFound", "Home");
            SetPassengers();
            return View(model);
        }

        // POST: VisasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VisaModel model)
        {
            if (!ModelState.IsValid)
            {
                SetPassengers();
                return View(model);
            }
            model.Id = id;
            try
            {
                await _service.UpdateAsync(model);
                TempData[Constants.SuccessFieldName] = "Edited successfully";
                return RedirectToAction(nameof(Details), new { model.Id });
            }
            catch (DbUpdateException exc)
            {
                SetPassengers();
                TempData[Constants.ErrorFieldName] = exc.InnerException != null ? exc.InnerException.Message : exc.Message;
                return View(model);
            }
            catch (ArgumentException exc)
            {
                SetPassengers();
                TempData[Constants.ErrorFieldName] = exc.Message;
                return View(model);
            }
            catch (InvalidOperationException)
            {
                return RedirectToAction("PageNotFound", "Home");
            }
        }

        // GET: VisasController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _service.GetByIdAsync(id);
            if (model is null)
                return RedirectToAction("PageNotFound", "Home");
            return View(model);
        }

        // POST: VisasController/Delete/5
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
