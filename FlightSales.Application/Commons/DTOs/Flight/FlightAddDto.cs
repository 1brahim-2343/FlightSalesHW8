using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSales.Application.Commons.DTOs.Flight
{
    public class FlightAddDto
    {
        public required string Origin { get; set; }
        public required string Destination { get; set; }
        public required string OriginCountry { get; set; }
        public required string DestinationCoutnry { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }

    }
}
