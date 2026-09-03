using FlightSales.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSales.Domain.Entities
{
    public class FlightTicket
    {
        public Guid Id { get; set; }
        public Guid FlightId { get; set; }
        public Flight Flight { get; set; }
        public Enums.Ticket Class { get; set; }
    }
}
