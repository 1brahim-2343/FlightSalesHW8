using FlightSales.Application.Commons.DTOs.Flight;
using FlightSales.Domain.Enums;

namespace FlightSales.Application.Commons.DTOs.Ticket
{
    public class FlightTicketDto
    {
        public Guid Id { get; set; }
        public TicketClass Class { get; set; }
        public required string PassengerFullname { get; set; }
        public decimal Price { get; set; }
        public required string Origin { get; set; }
        public required string Destination { get; set; }
        public DateTime DepartureTime { get; set; }
        public required FlightDto Flight { get; set; }

    }
}
