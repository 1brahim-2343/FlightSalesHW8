

namespace FlightSales.Application.Commons.DTOs.Flight
{
    public class FlightAddDto
    {
        public required string Origin { get; set; }
        public required string Destination { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }

    }
}
