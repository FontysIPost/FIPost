using LocatieService.Database.Datamodels;
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
    [Route("api/buildings")]
    [Authorize]
    public class BuildingController : Controller
    {
        private readonly IBuildingService _service;

        public BuildingController(IBuildingService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize(Roles = "0")]
        public async Task<ActionResult<BuildingResponse>> AddBuilding(BuildingRequest request)
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
        public async Task<ActionResult<List<BuildingResponse>>> GetAllBuildings([FromHeader] string Authorization)
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<BuildingResponse>> GetBuildingById(Guid id)
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
        public async Task<ActionResult<BuildingResponse>> GetBuildingByName(string name)
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
        [Authorize(Roles = "0")]
        [Route("{id}")]
        public async Task<ActionResult<BuildingResponse>> UpdateBuilding(Guid id, BuildingRequest request)
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
        [Authorize(Roles = "0")]
        [Route("{id}")]
        public async Task<ActionResult<BuildingResponse>> DeleteBuildingById(Guid id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
