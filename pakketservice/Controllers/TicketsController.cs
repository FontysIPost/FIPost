using Microsoft.AspNetCore.Mvc;
using PakketService.Database.Datamodels.Dtos;
using PakketService.helpers;
using PakketService.Services;
using System.Threading.Tasks;

namespace PakketService.Controllers
{
    [Route("api/tickets")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _service;

        public TicketsController(ITicketService service)
        {
            _service = service;
        }

        // POST: api/Packages
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TicketResponse>> AddTicket(TicketRequest request)
        {
            try
            {
                return Ok(await _service.AddAsync(request));
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
