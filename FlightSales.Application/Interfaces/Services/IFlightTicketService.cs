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
        Task<List<FlightTicket>> GetAsync();
        Task<FlightTicket?> GetAsync(Guid id);
        Task<bool> DeleteAsync(FlightTicket ticket);
        Task<FlightTicket> UpdateAsync(FlightTicket ticket);
    }
}