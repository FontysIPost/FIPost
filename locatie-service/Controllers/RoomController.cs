using LocatieService.Database.Datamodels.Dtos;
using LocatieService.Helpers;
using LocatieService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocatieService.Controllers
{
    [ApiController]
    [Route("api/rooms")]
    [Authorize]
    public class RoomController : Controller
    {
        private readonly IRoomService _service;

        public RoomController(IRoomService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize(Roles = "0")]
        public async Task<ActionResult<RoomResponse>> AddRoom(RoomRequest request)
        {
            try
            {
                return Ok(await _service.AddAsync(request));
            }
            catch (DuplicateException e)
            {
                return Conflict(e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<RoomResponse>>> GetAllRooms()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<RoomResponse>> GetRoomById(Guid id)
        {
            try
            {
                return Ok(await _service.GetByIdAsync(id));
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet]
        [Route("name/{name}")]
        public async Task<ActionResult<RoomResponse>> GetRoomByName(string name)
        {
            try
            {
                return Ok(await _service.GetByNameAsync(name));
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "0")]
        public async Task<ActionResult<RoomResponse>> UpdateRoom(Guid id, RoomRequest request)
        {
            try
            {
                return Ok(await _service.UpdateAsync(id, request));
            }
            catch (DuplicateException e)
            {
                return Conflict(e.Message);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "0")]
        public async Task<ActionResult<RoomResponse>> DeleteRoomById(Guid id)
        {
            try
            {
                await _service.DeleteRoomAsync(id);
                return NoContent();
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
