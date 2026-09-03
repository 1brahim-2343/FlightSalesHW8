using FlightSales.Domain.Entities;
using FlightSales.Infrastructure.Persistence;
using FlightSales.Infrastructure.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;


namespace FlightSales.Infrastructure.Repositories
{
    public class FlightTicketRepository : IFlightTicketRepository
    {
        private readonly FlightSalesDbContext _context;

        public FlightTicketRepository(FlightSalesDbContext context)
        {
            _context = context;
        }


        public async Task<FlightTicket> AddAsync(FlightTicket ticket)
        {
            var createdTicket = (await _context.AddAsync(ticket)).Entity;
            return createdTicket;
        }

        public async Task<bool> DeleteAsync(FlightTicket ticket)
        {
            _context.Tickets.Remove(ticket);

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<List<FlightTicket>> GetAsync()
        {
            return await _context.Tickets.ToListAsync();
        }

        public async Task<FlightTicket?> GetAsync(Guid id)
        {
            var ticket = await _context.Tickets.SingleOrDefaultAsync(t => t.Id == id);
            return ticket;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<FlightTicket> UpdateAsync(FlightTicket ticket)
        {
            var updatedTicket = _context.Update(ticket).Entity;
            await _context.SaveChangesAsync();
            return updatedTicket;
        }
    }
}
