using FlightSales.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSales.Application.Commons.DTOs.Ticket
{
    public class FlightTicketAddDto
    {
        public Guid FlightId { get; set; }
        public TicketClass Class { get; set; }
        public string PassengerFullname { get; set; }
        public decimal Price { get; set; }
    }
}
