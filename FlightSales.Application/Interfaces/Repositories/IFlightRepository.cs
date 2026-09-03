using FlightSales.Application.Commons.Models;
using FlightSales.Domain.Entities;


namespace FlightSales.Infrastructure.Repositories.Abstract
{
    public interface IFlightRepository
    {
        Task<List<Flight>> GetAsync();
        Task<Flight?> GetAsync(Guid id);
        Task<bool> DeleteAsync(Flight flight);
        Task<Flight> UpdateAsync(Flight flight);
        Task<Flight> AddAsync(Flight flight);
        Task<bool> SaveChangesAsync();
        Task<PagedResult<Flight>> GetAllPagedResultAsync(int pageNumber, int pageSize);
    }
}
