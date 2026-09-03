using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSales.Application.Commons.DTOs.Flight
{
    public class FlightDto
    {
        public required string Origin { get; set; }
        public required string Destination { get; set; }
        public required string OriginCountry { get; set; }
        public required string DestinationCountry { get; set; }
        public required string DestinationCity { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public TimeSpan FlightTime => ArrivalTime - DepartureTime;
    }
}
