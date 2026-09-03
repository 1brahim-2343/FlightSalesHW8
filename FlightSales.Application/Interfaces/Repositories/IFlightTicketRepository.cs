using FlightSales.Domain.Entities;


namespace FlightSales.Infrastructure.Repositories.Abstract
{
    public interface IFlightTicketRepository
    {
        Task<List<FlightTicket>> GetAsync();
        Task<FlightTicket?> GetAsync(Guid id);
        Task<bool> DeleteAsync(FlightTicket ticket);
        Task<FlightTicket> UpdateAsync(FlightTicket ticket);
        Task<FlightTicket> AddAsync(FlightTicket ticket);
        Task<bool> SaveChangesAsync();
    }
}
