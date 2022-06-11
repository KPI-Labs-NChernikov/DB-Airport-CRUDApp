using Business.Models;

namespace Business.Interfaces
{
    public interface IBaggageService : ICRUD<BaggageModel>, IModelValidator<BaggageModel>
    {    }
}
