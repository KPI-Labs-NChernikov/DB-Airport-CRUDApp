using Business.Models;

namespace Business.Interfaces
{
    public interface ITicketService : ICRUD<TicketModel>, IModelValidator<TicketModel>
    {    }
}
