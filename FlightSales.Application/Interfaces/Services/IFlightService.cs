using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSales.Application.Commons.Models;
using FlightSales.Domain.Entities;

namespace FlightSales.Application.Interfaces.Services
{
    public interface IFlightService
    {
        Task<List<Flight>> GetAsync();
        Task<Flight?> GetAsync(Guid id);
        Task<PagedResult<Flight>> GetAllPagedAsync(int pageNumber, int pageSize);
        Task<bool> DeleteAsync(Flight flight);
        Task<Flight> AddAsync(Flight flight);
        Task<Flight> UpdateAsync(Flight flight);
        string GetDestinationCity(Flight flight);
        string GetDestinationCountry(Flight flight);
        string GetOriginCity(Flight flight);
        string GetOriginCountry(Flight flight);
    }
}
