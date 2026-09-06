using AutoMapper;
using FlightSales.Application.Commons.DTOs.Flight;
using FlightSales.Application.Interfaces.Services;
using FlightSales.Domain.Entities;


namespace FlightSales.Application.Mappers.FlightResolvers
{
    public class DestinationCountryResolver : IValueResolver<Flight, FlightDto, string>
    {
        private readonly IFlightService _flightService;

        public DestinationCountryResolver(IFlightService flightService)
        {
            _flightService = flightService;
        }

        public string Resolve(Flight source, FlightDto destination, string destMember, ResolutionContext context)
        {
            return _flightService.GetDestinationCountry(source);
        }
    }
}
