using FlightSales.Application.Interfaces.Repositories;
using FlightSales.Application.Interfaces.Services;
using FlightSales.Domain.Entities;

namespace FlightSales.Infrastructure.Services;

public class FlightService : IFlightService
{
    private readonly IFlightRepository _flightRepo;

    public FlightService(IFlightRepository flightRepo)
    {
        _flightRepo = flightRepo;
    }

    public async Task<List<Flight>> GetAsync()
    {
        return await _flightRepo.GetAsync();
    }

    public async Task<Flight?> GetAsync(Guid id)
    {
        return await _flightRepo.GetAsync(id);
    }

    public async Task<bool> DeleteAsync(Flight flight)
    {
        return await _flightRepo.DeleteAsync(flight);
    }


    public async Task<Flight> AddAsync(Flight flight)
    {
        var newFlight = await _flightRepo.AddAsync(flight);
        await _flightRepo.SaveChangesAsync();
        return newFlight;
    }

    public async Task<Flight> UpdateAsync(Flight flight)
    {
        return await _flightRepo.UpdateAsync(flight);
    }

    public string GetDestinationCity(Flight flight)
    {
        throw new NotImplementedException();
    }
}