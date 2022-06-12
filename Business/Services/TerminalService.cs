using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Data;
using Data.Models;

namespace Business.Services
{
    public class TerminalService : ITerminalService
    {
        private readonly AirportContext _context;

        private readonly IMapper _mapper;

        public TerminalService(AirportContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private async Task<Terminal> GetNotMappedByIdAsync(int id)
        {
            var model = await _context.Terminals.FindAsync(id);
            if (model is null)
                throw new InvalidOperationException("Model with such an id was not found");
            return model;
        }

        public async Task AddAsync(TerminalModel model)
        {
            bool isValid = IsValid(model, out string? error);
            if (!isValid)
                throw new ArgumentException(error, nameof(model));
            var mapped = _mapper.Map<Terminal>(model);
            await _context.Terminals.AddAsync(mapped);
            await _context.SaveChangesAsync();
            model.Id = mapped.Id;
        }

        public async Task DeleteByIdAsync(int id)
        {
            _context.Terminals.Remove(await GetNotMappedByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        public IEnumerable<TerminalModel> GetAll() => _mapper.Map<IEnumerable<TerminalModel>>(_context.Terminals);

        public async Task<TerminalModel?> GetByIdAsync(int id)
        {
            return _mapper.Map<TerminalModel>(await _context.Terminals.FindAsync(id));
        }

        public bool IsValid(TerminalModel model, out string? errorMessage)
        {
            var result = IModelValidator<TerminalModel>.IsValidByDefault(model, out errorMessage);
            return result;
        }

        public async Task UpdateAsync(TerminalModel model)
        {
            bool isValid = IsValid(model, out string? error);
            if (!isValid)
                throw new ArgumentException(error, nameof(model));
            var existingModel = await GetNotMappedByIdAsync(model.Id);
            existingModel = _mapper.Map(model, existingModel);
            _context.Terminals.Update(existingModel);
            await _context.SaveChangesAsync();
        }
    }
}
