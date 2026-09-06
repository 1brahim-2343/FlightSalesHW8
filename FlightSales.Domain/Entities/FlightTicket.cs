using FlightSales.Domain.Enums;


namespace FlightSales.Domain.Entities
{
    public class FlightTicket
    {
        public Guid Id { get; set; }
        public Guid FlightId { get; set; }
        public Flight Flight { get; set; }
        public TicketClass Class { get; set; }
        public string SeatNumber { get; set; }
        public string PassengerFullname { get; set; }
        public decimal Price { get; set; }
    }
}
