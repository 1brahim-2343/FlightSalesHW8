using FlightSales.Domain.Entities;

namespace FlightSales.Application.Interfaces.Repositories
{
    public interface IFlightTicketRepository
    {
        Task<FlightTicket?> GetAsync(Guid id);
        Task<bool> DeleteAsync(FlightTicket ticket);
        Task<FlightTicket> UpdateAsync(FlightTicket ticket);
        Task<FlightTicket> AddAsync(FlightTicket ticket);
        Task<bool> SaveChangesAsync();
    }
}
