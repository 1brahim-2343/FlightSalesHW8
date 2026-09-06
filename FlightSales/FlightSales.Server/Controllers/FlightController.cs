using AutoMapper;
using FlightSales.Application.Commons.DTOs.Flight;
using FlightSales.Application.Commons.Models;
using FlightSales.Application.Interfaces.Services;
using FlightSales.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace FlightSales.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;

        public FlightController(IFlightService flightService, IMapper mapper)
        {
            _flightService = flightService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FlightDto>>> GetAllAsync()
        {
            var flights = await _flightService.GetAsync();
            var flightsDto = _mapper.Map<IEnumerable<FlightDto>>(flights);

            return Ok(flightsDto);
        }

        [HttpGet("{id:guid}", Name = "GetByIdAsync")]
        public async Task<ActionResult<FlightDto>> GetByIdAsync(Guid id)
        {
            var flight = await _flightService.GetAsync(id);
            if (flight == null) return NotFound();

            var flightDto = _mapper.Map<FlightDto>(flight);

            return Ok(flightDto);
        }

        [HttpGet("paged")]
        public async Task<ActionResult<PagedResult<Flight>>> GetPagedAsync(int pageNumber = 1, int pageSize = 10)
        {
            var serviceFlights = await _flightService.GetAllPagedAsync(pageNumber, pageSize);

            return Ok(serviceFlights);
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync(FlightAddDto flightDto)
        {
            if (flightDto == null) return BadRequest("Body can not be null");

            var flight = _mapper.Map<Flight>(flightDto);

            try
            {
                ValidateFlight(flight);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

            var newFlight = await _flightService.AddAsync(flight);

            var newFlightDto = _mapper.Map<FlightDto>(newFlight);

            return CreatedAtRoute("GetByIdAsync", new
            {
                id = newFlight.Id
            }, newFlightDto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] FlightUpdateDto dto)
        {
            if (dto == null) return BadRequest("Body can not be null");
            try
            {
                var flight = await _flightService.GetAsync(id);
                if (flight == null) return NotFound();

                _mapper.Map(dto, flight);

                var updatedAirplane = await _flightService.UpdateAsync(flight);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var flight = await _flightService.GetAsync(id);
            if (flight == null) return NotFound();

            var deletedFlight = await _flightService.DeleteAsync(flight);

            return Ok(deletedFlight);
        }

        private static void ValidateFlight(Flight flight)
        {
            var pattern = @"^[A-Z]{3} \| [A-Z]+(?: [A-Z]+)*, [A-Z]+(?: [A-Z]+)*$";
            if (!Regex.IsMatch(flight.Origin, pattern))
            {
                throw new ArgumentException("Origin point does not match format");
            }
            if (!Regex.IsMatch(flight.Destination, pattern))
            {
                throw new ArgumentException("Destination point does not match format");
            }
            if (flight.DepartureTime > flight.ArrivalTime)
            {
                throw new ArgumentException("Departure time must be earlier than arrival time");
            }
        }
    }
}
