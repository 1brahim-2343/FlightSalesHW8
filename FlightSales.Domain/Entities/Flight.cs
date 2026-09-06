

namespace FlightSales.Domain.Entities
{
    public class Flight
    {
        public Guid Id { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
    }
}
