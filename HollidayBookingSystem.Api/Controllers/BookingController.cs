using HollidayBookingSystem.Application.Commands;
using HollidayBookingSystem.Application.Handlers;
using HollidayBookingSystem.Domain.Entities;
using HollidayBookingSystem.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HollidayBookingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly BookingCommandHandler _handler;
        private readonly IBookingRepository _repository;

        public BookingController(BookingCommandHandler handler, IBookingRepository repository)
        {
            _handler = handler;
            _repository = repository;
        }

        // GET: api/<BookingController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<BookingController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public async Task<IActionResult> Add(Booking booking)
        {
            await _handler.HandleAsync(new AddBookingCommand(_repository, booking));
            return Ok(booking);
        }

        // PUT api/<BookingController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BookingController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
