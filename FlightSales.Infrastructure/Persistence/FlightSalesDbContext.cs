using FlightSales.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace FlightSales.Infrastructure.Persistence
{
    public class FlightSalesDbContext : DbContext
    {
        public FlightSalesDbContext(
            DbContextOptions<FlightSalesDbContext> options) : base(options)
        { }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<FlightTicket> Tickets { get; set; }
    }
}
