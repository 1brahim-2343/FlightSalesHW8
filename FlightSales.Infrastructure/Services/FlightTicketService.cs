using FlightSales.Application.Interfaces.Repositories;
using FlightSales.Application.Interfaces.Services;
using FlightSales.Domain.Entities;

namespace FlightSales.Infrastructure.Services;

public class FlightTicketService : IFlightTicketService
{
    private readonly IFlightTicketRepository _flightTicketRepo;

    public FlightTicketService(IFlightTicketRepository flightTicketRepo)
    {
        _flightTicketRepo = flightTicketRepo;
    }

    public async Task<FlightTicket> AddAsync(FlightTicket ticket)
    {
        var addedTicket = await _flightTicketRepo.AddAsync(ticket);
        await _flightTicketRepo.SaveChangesAsync();
        return addedTicket;
    }

    public async Task<bool> DeleteAsync(FlightTicket ticket)
    {
        return await _flightTicketRepo.DeleteAsync(ticket);
    }


    public async Task<FlightTicket?> GetAsync(Guid id)
    {
        return await _flightTicketRepo.GetAsync(id);
    }

    public async Task<FlightTicket> UpdateAsync(FlightTicket ticket)
    {
        return await _flightTicketRepo.UpdateAsync(ticket);
    }
}