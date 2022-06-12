using AutoMapper;
using Business.Models;
using Data.Models;

namespace Business
{
    public class AutoMapperBusinessProfile : Profile
    {
        public AutoMapperBusinessProfile()
        {
            CreateMap<Baggage, BaggageModel>().ReverseMap();
            CreateMap<Flight, FlightModel>().ReverseMap();
            CreateMap<Passenger, PassengerModel>().ReverseMap();
            CreateMap<Plane, PlaneModel>().ReverseMap();
            CreateMap<Terminal, TerminalModel>().ReverseMap();
            CreateMap<Ticket, TicketModel>().ReverseMap();
            CreateMap<Visa, VisaModel>().ReverseMap();
        }
    }
}
