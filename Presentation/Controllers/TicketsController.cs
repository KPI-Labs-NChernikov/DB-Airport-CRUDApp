using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ITicketService _service;

        private readonly IPassengerService _passengerService;

        private readonly IFlightService _flightService;

        private readonly IMapper _mapper;

        public TicketsController(ITicketService service, IPassengerService passengerService, IFlightService flightService, IMapper mapper)
        {
            _service = service;
            _passengerService = passengerService;
            _flightService = flightService;
            _mapper = mapper;
        }

        private void SetPassengers()
        {
            ViewBag.Passengers = _mapper.Map<IEnumerable<PassengerReduced>>(_passengerService.GetAll());
        }

        private void SetFlights()
        {
            ViewBag.Flights = _mapper.Map<IEnumerable<FlightReduced>>(_flightService.GetAll());
        }

        private void SetViewBag()
        {
            SetPassengers();
            SetFlights();
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
            SetViewBag();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TicketModel model)
        {
            if (!ModelState.IsValid)
            {
                SetViewBag();
                return View(model);
            }
            try
            {
                await _service.AddAsync(model);
                TempData[Constants.SuccessFieldName] = "Created successfully";
                return RedirectToAction(nameof(Details), new { model.Id });
            }
            catch (DbUpdateException exc)
            {
                SetViewBag();
                TempData[Constants.ErrorFieldName] = exc.InnerException != null ? exc.InnerException.Message : exc.Message;
                return View(model);
            }
            catch (ArgumentException exc)
            {
                SetViewBag();
                TempData[Constants.ErrorFieldName] = exc.Message;
                return View(model);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _service.GetByIdAsync(id);
            if (model is null)
                return RedirectToAction("PageNotFound", "Home");
            SetViewBag();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TicketModel model)
        {
            if (!ModelState.IsValid)
            {
                SetViewBag();
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
                SetViewBag();
                TempData[Constants.ErrorFieldName] = exc.InnerException != null ? exc.InnerException.Message : exc.Message;
                return View(model);
            }
            catch (ArgumentException exc)
            {
                SetViewBag();
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
