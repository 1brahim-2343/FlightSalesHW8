using AutoMapper;
using FlightSales.Application.Commons.DTOs.Flight;
using FlightSales.Application.Commons.DTOs.Ticket;
using FlightSales.Application.Interfaces.Services;
using FlightSales.Domain.Entities;
using FlightSales.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightSales.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightTicketController : ControllerBase
    {
        private readonly IFlightTicketService _ticketService;
        private readonly IMapper _mapper;

        public FlightTicketController(IFlightTicketService ticketService, IMapper mapper)
        {
            _ticketService = ticketService;
            _mapper = mapper;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<IEnumerable<FlightTicketDto>>> GetAsync(Guid id)
        {
            var serviceFlightTickets = await _ticketService.GetAsync(id);
            var flightsTicketsDto = _mapper.Map<IEnumerable<FlightTicketDto>>(serviceFlightTickets);

            return Ok(flightsTicketsDto);
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync(FlightTicketAddDto dto)
        {
            if (dto == null) return BadRequest("Body can not be null");

            var flightTicket = _mapper.Map<FlightTicket>(dto);

           
            var newFlightTicket = await _ticketService.AddAsync(flightTicket);

            var newFlightTicketDto = _mapper.Map<FlightTicketDto>(newFlightTicket);

            return CreatedAtRoute("GetAsync", new
            {
                id = newFlightTicket.Id
            }, newFlightTicketDto);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var flightTicket = await _ticketService.GetAsync(id);
            if (flightTicket == null) return NotFound();

            var deletedTicket = await _ticketService.DeleteAsync(flightTicket);

            return Ok(deletedTicket);
        }

    }
}
