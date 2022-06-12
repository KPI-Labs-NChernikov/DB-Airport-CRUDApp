using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Data;
using Data.Models;

namespace Business.Services
{
    public class TicketService : ITicketService
    {
        private readonly AirportContext _context;

        private readonly IMapper _mapper;

        public TicketService(AirportContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private async Task<Ticket> GetNotMappedByIdAsync(int id)
        {
            var model = await _context.Tickets.FindAsync(id);
            if (model is null)
                throw new InvalidOperationException("Model with such an id was not found");
            return model;
        }

        public async Task AddAsync(TicketModel model)
        {
            bool isValid = IsValid(model, out string? error);
            if (!isValid)
                throw new ArgumentException(error, nameof(model));
            TransformModel(model);
            var mapped = _mapper.Map<Ticket>(model);
            await _context.Tickets.AddAsync(mapped);
            await _context.SaveChangesAsync();
            model.Id = mapped.Id;
        }

        public async Task DeleteByIdAsync(int id)
        {
            _context.Tickets.Remove(await GetNotMappedByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        public IEnumerable<TicketModel> GetAll() => _mapper.Map<IEnumerable<TicketModel>>(_context.Tickets);

        public async Task<TicketModel?> GetByIdAsync(int id)
        {
            return _mapper.Map<TicketModel>(await _context.Tickets.FindAsync(id));
        }

        private static void TransformModel(TicketModel model)
        {
            model.Code = model.Code.ToUpper();
        }

        public bool IsValid(TicketModel model, out string? errorMessage)
        {
            var result = IModelValidator<TicketModel>.IsValidByDefault(model, out errorMessage);
            return result;
        }

        public async Task UpdateAsync(TicketModel model)
        {
            bool isValid = IsValid(model, out string? error);
            if (!isValid)
                throw new ArgumentException(error, nameof(model));
            TransformModel(model);
            var existingModel = await GetNotMappedByIdAsync(model.Id);
            existingModel = _mapper.Map(model, existingModel);
            _context.Tickets.Update(existingModel);
            await _context.SaveChangesAsync();
        }
    }
}
