using Business.Models;

namespace Business.Interfaces
{
    public interface IFlightService : ICRUD<FlightModel>, IModelValidator<FlightModel>
    {    }
}
