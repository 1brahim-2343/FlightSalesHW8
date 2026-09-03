using FlightSales.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSales.Application.Commons.DTOs.Ticket
{
    public class FlightTicketDto
    {
        public TicketClass Class { get; set; }
        public required string PassengerFullname { get; set; }
        public decimal Price { get; set; }
        public required string Origin { get; set; }
        public required string Destination { get; set; }
        public DateTime DepartureTime { get; set; }

    }
}
