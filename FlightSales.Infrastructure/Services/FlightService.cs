using FlightSales.Application.Commons.Models;
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
        var destination = flight.Destination;
        var destinationCity = destination
            .Split("|")[1].Trim()
            .Split(",")[0].Trim();
        return destinationCity;
    }

    public string GetDestinationCountry(Flight flight)
    {
        var destination = flight.Destination;
        var destinationCountry = destination
            .Split("|")[1].Trim()
            .Split(",")[1].Trim();
        return destinationCountry;
    }

    public string GetOriginCity(Flight flight)
    {
        var origin = flight.Origin;
        var originCity = origin
            .Split("|")[1].Trim()
            .Split(",")[0].Trim();
        return originCity;
    }

    public string GetOriginCountry(Flight flight)
    {
        var origin = flight.Origin;
        var originCountry = origin
            .Split("|")[1].Trim()
            .Split(",")[1].Trim();
        return originCountry;
    }

    public async Task<PagedResult<Flight>> GetAllPagedAsync(int pageNumber, int pageSize)
    {
        return await _flightRepo.GetAllPagedResultAsync(pageNumber, pageSize);
    }
}