using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Data;
using Data.Models;

namespace Business.Services
{
    public class FlightService : IFlightService
    {
        private readonly AirportContext _context;

        private readonly IMapper _mapper;

        public FlightService(AirportContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private async Task<Flight> GetNotMappedByIdAsync(int id)
        {
            var model = await _context.Flights.FindAsync(id);
            if (model is null)
                throw new InvalidOperationException("Model with such an id was not found");
            return model;
        }

        public async Task AddAsync(FlightModel model)
        {
            bool isValid = IsValid(model, out string? error);
            if (!isValid)
                throw new ArgumentException(error, nameof(model));
            var mapped = _mapper.Map<Flight>(model);
            await _context.Flights.AddAsync(mapped);
            await _context.SaveChangesAsync();
            model.Id = mapped.Id;
        }

        public async Task DeleteByIdAsync(int id)
        {
            _context.Flights.Remove(await GetNotMappedByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        public IEnumerable<FlightModel> GetAll() => _mapper.Map<IEnumerable<FlightModel>>(_context.Flights);

        public async Task<FlightModel?> GetByIdAsync(int id)
        {
            return _mapper.Map<FlightModel>(await _context.Flights.FindAsync(id));
        }

        public bool IsValid(FlightModel model, out string? errorMessage)
        {
            var result = IModelValidator<FlightModel>.IsValidByDefault(model, out errorMessage);
            return result;
        }

        public async Task UpdateAsync(FlightModel model)
        {
            bool isValid = IsValid(model, out string? error);
            if (!isValid)
                throw new ArgumentException(error, nameof(model));
            var existingModel = await GetNotMappedByIdAsync(model.Id);
            existingModel = _mapper.Map(model, existingModel);
            _context.Flights.Update(existingModel);
            await _context.SaveChangesAsync();
        }
    }
}
