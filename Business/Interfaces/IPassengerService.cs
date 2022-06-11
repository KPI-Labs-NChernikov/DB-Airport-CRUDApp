using Business.Models;

namespace Business.Interfaces
{
    public interface IPassengerService : ICRUD<PassengerModel>, IModelValidator<PassengerModel>
    {    }
}
