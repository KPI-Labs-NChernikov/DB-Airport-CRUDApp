using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Data;
using Data.Models;

namespace Business.Services
{
    public class BaggageService : IBaggageService
    {
        private readonly AirportContext _context;

        private readonly IMapper _mapper;

        public BaggageService(AirportContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private async Task<Baggage> GetNotMappedByIdAsync(int id)
        {
            var model = await _context.Baggages.FindAsync(id);
            if (model is null)
                throw new InvalidOperationException("Model with such an id was not found");
            return model;
        }

        public async Task AddAsync(BaggageModel model)
        {
            bool isValid = IsValid(model, out string? error);
            if (!isValid)
                throw new ArgumentException(error, nameof(model));
            var mapped = _mapper.Map<Baggage>(model);
            await _context.Baggages.AddAsync(mapped);
            await _context.SaveChangesAsync();
            model.Id = mapped.Id;
        }

        public async Task DeleteByIdAsync(int id)
        {
            _context.Baggages.Remove(await GetNotMappedByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        public IEnumerable<BaggageModel> GetAll() => _mapper.Map<IEnumerable<BaggageModel>>(_context.Baggages);

        public async Task<BaggageModel?> GetByIdAsync(int id)
        {
            return _mapper.Map<BaggageModel>(await _context.Baggages.FindAsync(id));
        }

        public bool IsValid(BaggageModel model, out string? errorMessage)
        {
            var result = IModelValidator<BaggageModel>.IsValidByDefault(model, out errorMessage);
            return result;
        }

        public async Task UpdateAsync(BaggageModel model)
        {
            bool isValid = IsValid(model, out string? error);
            if (!isValid)
                throw new ArgumentException(error, nameof(model));
            var existingModel = await GetNotMappedByIdAsync(model.Id);
            existingModel = _mapper.Map(model, existingModel);
            _context.Baggages.Update(existingModel);
            await _context.SaveChangesAsync();
        }
    }
}
