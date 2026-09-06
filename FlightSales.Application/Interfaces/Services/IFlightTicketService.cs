using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSales.Domain.Entities;

namespace FlightSales.Application.Interfaces.Services
{
    public interface IFlightTicketService
    {
        Task<FlightTicket?> GetAsync(Guid id);
        Task<FlightTicket> AddAsync(FlightTicket ticket);
        Task<bool> DeleteAsync(FlightTicket ticket);
    }
}