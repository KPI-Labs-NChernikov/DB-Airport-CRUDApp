using Business.Models;

namespace Business.Interfaces
{
    public interface IPlaneService : ICRUD<PlaneModel>, IModelValidator<PlaneModel>
    {    }
}
