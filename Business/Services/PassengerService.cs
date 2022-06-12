using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Data;
using Data.Models;

namespace Business.Services
{
    public class PassengerService : IPassengerService
    {
        private readonly AirportContext _context;

        private readonly IMapper _mapper;

        public PassengerService(AirportContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private async Task<Passenger> GetNotMappedByIdAsync(int id)
        {
            var model = await _context.Passengers.FindAsync(id);
            if (model is null)
                throw new InvalidOperationException("Model with such an id was not found");
            return model;
        }

        public async Task AddAsync(PassengerModel model)
        {
            bool isValid = IsValid(model, out string? error);
            if (!isValid)
                throw new ArgumentException(error, nameof(model));
            TransformModel(model);
            var mapped = _mapper.Map<Passenger>(model);
            await _context.Passengers.AddAsync(mapped);
            await _context.SaveChangesAsync();
            model.Id = mapped.Id;
        }

        public async Task DeleteByIdAsync(int id)
        {
            _context.Passengers.Remove(await GetNotMappedByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        public IEnumerable<PassengerModel> GetAll() => _mapper.Map<IEnumerable<PassengerModel>>(_context.Passengers);

        public async Task<PassengerModel?> GetByIdAsync(int id)
        {
            return _mapper.Map<PassengerModel>(await _context.Passengers.FindAsync(id));
        }

        private static void TransformModel(PassengerModel model)
        {
            model.PassportCode = model.PassportCode.ToUpper();
        }

        public bool IsValid(PassengerModel model, out string? errorMessage)
        {
            var result = IModelValidator<PassengerModel>.IsValidByDefault(model, out errorMessage);
            return result;
        }

        public async Task UpdateAsync(PassengerModel model)
        {
            bool isValid = IsValid(model, out string? error);
            if (!isValid)
                throw new ArgumentException(error, nameof(model));
            TransformModel(model);
            var existingModel = await GetNotMappedByIdAsync(model.Id);
            existingModel = _mapper.Map(model, existingModel);
            _context.Passengers.Update(existingModel);
            await _context.SaveChangesAsync();
        }
    }
}
