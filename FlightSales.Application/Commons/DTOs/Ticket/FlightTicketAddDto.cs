using FlightSales.Domain.Enums;

namespace FlightSales.Application.Commons.DTOs.Ticket
{
    public class FlightTicketAddDto
    {
        public Guid FlightId { get; set; }
        public TicketClass Class { get; set; }
        public required string SeatNumber { get; set; }
        public required string PassengerFullname { get; set; }
        public decimal Price { get; set; }
    }
}
