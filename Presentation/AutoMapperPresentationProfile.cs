using AutoMapper;
using Business.Models;
using Presentation.Models;

namespace Presentation
{
    public class AutoMapperPresentationProfile : Profile
    {
        public AutoMapperPresentationProfile()
        {
            CreateMap<PassengerModel, PassengerReduced>();
            CreateMap<FlightModel, FlightReduced>();
            CreateMap<PlaneModel, PlaneReduced>();
            CreateMap<TerminalModel, TerminalReduced>();
            CreateMap<TicketModel, TicketReduced>();
        }
    }
}
