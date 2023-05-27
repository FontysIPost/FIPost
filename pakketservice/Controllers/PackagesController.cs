using Microsoft.AspNetCore.Mvc;
using PakketService.Database.Converters;
using PakketService.Database.Datamodels;
using PakketService.Database.Datamodels.Dtos;
using PakketService.helpers;
using PakketService.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PakketService.Controllers
{
    [Route("api/packages")]
    [ApiController]
    public class PackagesController : ControllerBase
    {
        private readonly IPackageService _service;

        public PackagesController(IPackageService service)
        {
            _service = service;
        }

        // POST: api/Packages
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PackageResponse>> AddPackage(PackageRequest request)


        {
            DateTime Time = DateTime.Now;
            logData data = new logData(request.CreatedByPersonId, request.Name, request.ReceiverId, request.Sender, request.CreatedAtLocationId.ToString(), Time);
            await logData.LogToMicroserviceAsync(data, "https://localhost:44331/Logging/Pakketje");


            return Ok(await _service.AddAsync(request));
            //personID
            //Pakketname
            //ReceiverID
            //Sender Ammar
            //LocationID
            

        }

        [HttpGet]
        public async Task<ActionResult<List<PackageResponse>>> GetAllPackages()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PackageResponse>> GetPackageById(Guid id)
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

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<PackageResponse>> UpdatePackage(Guid id, PackageRequest request)
        {
            try
            {
                return Ok(await _service.UpdateAsync(id, request));
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet]
        [Route("health")]
        public ActionResult Health()
        {
            return Ok();
        }
    }
}
