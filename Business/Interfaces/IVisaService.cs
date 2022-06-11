using Business.Models;

namespace Business.Interfaces
{
    public interface IVisaService : ICRUD<VisaModel>, IModelValidator<VisaModel>
    {    }
}
