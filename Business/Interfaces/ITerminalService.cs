using Business.Models;

namespace Business.Interfaces
{
    public interface ITerminalService : ICRUD<TerminalModel>, IModelValidator<TerminalModel>
    {    }
}
