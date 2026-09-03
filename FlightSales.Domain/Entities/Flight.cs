using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSales.Domain.Entities
{
    public class Flight
    {
        public Guid Id { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string OriginCountry { get; set; }
        public string DestinationCoutnry { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
    }
}
