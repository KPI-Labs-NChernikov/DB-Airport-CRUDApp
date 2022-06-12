using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Data;
using Data.Models;

namespace Business.Services
{
    public class VisaService : IVisaService
    {
        private readonly AirportContext _context;

        private readonly IMapper _mapper;

        public VisaService(AirportContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private async Task<Visa> GetNotMappedByIdAsync(int id)
        {
            var model = await _context.Visas.FindAsync(id);
            if (model is null)
                throw new InvalidOperationException("Model with such an id was not found");
            return model;
        }

        public async Task AddAsync(VisaModel model)
        {
            bool isValid = IsValid(model, out string? error);
            if (!isValid)
                throw new ArgumentException(error, nameof(model));
            var mapped = _mapper.Map<Visa>(model);
            await _context.Visas.AddAsync(mapped);
            await _context.SaveChangesAsync();
            model.Id = mapped.Id;
        }

        public async Task DeleteByIdAsync(int id)
        {
            _context.Visas.Remove(await GetNotMappedByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        public IEnumerable<VisaModel> GetAll() => _mapper.Map<IEnumerable<VisaModel>>(_context.Visas);

        public async Task<VisaModel?> GetByIdAsync(int id)
        {
            return _mapper.Map<VisaModel>(await _context.Visas.FindAsync(id));
        }

        public bool IsValid(VisaModel model, out string? errorMessage)
        {
            var result = IModelValidator<VisaModel>.IsValidByDefault(model, out errorMessage);
            if (model.Expiry < model.Issue)
            {
                errorMessage += $"{Environment.NewLine}The Expire date must be bigger than or equal the Issue date";
                result = false;
            }
            errorMessage = errorMessage?.TrimStart();
            return result;
        }

        public async Task UpdateAsync(VisaModel model)
        {
            bool isValid = IsValid(model, out string? error);
            if (!isValid)
                throw new ArgumentException(error, nameof(model));
            var existingModel = await GetNotMappedByIdAsync(model.Id);
            existingModel = _mapper.Map(model, existingModel);
            _context.Visas.Update(existingModel);
            await _context.SaveChangesAsync();
        }
    }
}
