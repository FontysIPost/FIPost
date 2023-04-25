using LocatieService.Database.Datamodels;
using LocatieService.Database.Datamodels.Dtos;
using LocatieService.Helpers;
using LocatieService.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocatieService.Controllers
{
    [ApiController]
    [Route("api/buildings")]
    public class BuildingController : Controller
    {
        private readonly IBuildingService _service;

        public BuildingController(IBuildingService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<BuildingResponse>> AddBuilding(BuildingRequest request)
        {
            try
            {
                DateTime Time = DateTime.Now;
                logData data = new logData(request.Name, "Building", Time);


                await logData.LogToMicroserviceAsync(data, "https://localhost:44331/Logging/locatie");
                return Ok(await _service.AddAsync(request));
            }
            catch (DuplicateException e)
            {
                return Conflict(e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<BuildingResponse>>> GetAllBuildings()
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
