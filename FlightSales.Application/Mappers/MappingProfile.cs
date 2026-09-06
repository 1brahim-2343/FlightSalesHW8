using AutoMapper;
using FlightSales.Application.Commons.DTOs.Flight;
using FlightSales.Application.Commons.DTOs.Ticket;
using FlightSales.Application.Mappers.FlightResolvers;
using FlightSales.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSales.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region FlightMappers
            CreateMap<FlightAddDto, Flight>();
            CreateMap<Flight, FlightDto>()
                .ForMember(dest => dest.OriginCountry,
                opt => opt.MapFrom<OriginCountryResolver>())
                .ForMember(dest => dest.DestinationCountry,
                opt => opt.MapFrom<DestinationCountryResolver>())
                .ForMember(dest => dest.DestinationCity,
                opt => opt.MapFrom<DestinationCityResolver>());
            CreateMap<FlightUpdateDto, Flight>();
            #endregion

            #region FlightTicketMappers
            CreateMap<FlightTicket, FlightTicketDto>();
            CreateMap<FlightTicketAddDto, FlightTicket>();
            #endregion


        }
    }
}
