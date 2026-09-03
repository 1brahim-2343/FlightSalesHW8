using FlightSales.Application.Commons.Models;
using FlightSales.Domain.Entities;
using FlightSales.Infrastructure.Persistence;
using FlightSales.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FlightSales.Infrastructure.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        private readonly FlightSalesDbContext _context;


        public FlightRepository(FlightSalesDbContext context)
        {
            _context = context;
        }


        public async Task<Flight> AddAsync(Flight flight)
        {
            var createdFlight = (await _context.AddAsync(flight)).Entity;
            return createdFlight;
        }

        public async Task<bool> DeleteAsync(Flight flight)
        {
            _context.Flights.Remove(flight);

           return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<PagedResult<Flight>> GetAllPagedResultAsync(int pageNumber, int pageSize)
        {
            var query = _context.Flights;

            var totalCount = await query.CountAsync();

            var flights = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Flight>
            {
                Items = flights,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public Task<List<Flight>> GetAsync()
        {
            return _context.Flights.ToListAsync();
        }

        public async Task<Flight?> GetAsync(Guid id)
        {
            return await _context.Flights.SingleOrDefaultAsync(f => f.Id == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Flight> UpdateAsync(Flight flight)
        {
            var updatedFlight = _context.Update(flight).Entity;
            await _context.SaveChangesAsync();
            return updatedFlight;
        }
    }
}
