using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Data;
using Data.Models;

namespace Business.Services
{
    public class PlaneService : IPlaneService
    {
        private readonly AirportContext _context;

        private readonly IMapper _mapper;

        public PlaneService(AirportContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private async Task<Plane> GetNotMappedByIdAsync(int id)
        {
            var model = await _context.Planes.FindAsync(id);
            if (model is null)
                throw new InvalidOperationException("Model with such an id was not found");
            return model;
        }

        public async Task AddAsync(PlaneModel model)
        {
            bool isValid = IsValid(model, out string? error);
            if (!isValid)
                throw new ArgumentException(error, nameof(model));
            TransformModel(model);
            var mapped = _mapper.Map<Plane>(model);
            await _context.Planes.AddAsync(mapped);
            await _context.SaveChangesAsync();
            model.Id = mapped.Id;
        }

        public async Task DeleteByIdAsync(int id)
        {
            _context.Planes.Remove(await GetNotMappedByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        public IEnumerable<PlaneModel> GetAll() => _mapper.Map<IEnumerable<PlaneModel>>(_context.Planes);

        public async Task<PlaneModel?> GetByIdAsync(int id)
        {
            return _mapper.Map<PlaneModel>(await _context.Planes.FindAsync(id));
        }

        private static void TransformModel(PlaneModel model)
        {
            model.RegistrationNumber = model.RegistrationNumber.ToUpper();
        }

        public bool IsValid(PlaneModel model, out string? errorMessage)
        {
            var result = IModelValidator<PlaneModel>.IsValidByDefault(model, out errorMessage);
            return result;
        }

        public async Task UpdateAsync(PlaneModel model)
        {
            bool isValid = IsValid(model, out string? error);
            if (!isValid)
                throw new ArgumentException(error, nameof(model));
            TransformModel(model);
            var existingModel = await GetNotMappedByIdAsync(model.Id);
            existingModel = _mapper.Map(model, existingModel);
            _context.Planes.Update(existingModel);
            await _context.SaveChangesAsync();
        }
    }
}
