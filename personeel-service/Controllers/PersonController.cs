using Microsoft.AspNetCore.Mvc;
using personeel_service.Database.Contexts;
using personeel_service.Helpers;
using personeel_service.Models;
using personeel_service.Models.DTO_s;
using personeel_service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace personeel_service.Controllers
{
    [Route("api/persons")]
    [ApiController]
    public class PersonController : Controller
    {

        private readonly IPersonService _service;

        /* public PersonController(IPersonService service)
         {
             _service = service;
         }*/


        public PersonController(IPersonService service)
        {
            _service = service;
        }


        // GET: api/Persons
        //[HttpGet]
        /*  public ActionResult<IEnumerable<Person>> GetPerson()
          {
              return Ok(_service.GetAll());
          }*/
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                return Ok(await _service.GetAllAsync());

            }
            catch (NotFoundException e)
            {

                return NotFound(e.Message);
            }
        }




        // GET: api/Persons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonResponse>> GetPerson(string id)
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

        // GET: api/Persons
        [HttpGet]
        [Route("health")]
        public ActionResult Health()
        {
            return Ok();
        }

        [HttpGet]
        [Route("getbyfontysid/{id}")]
        public async Task<ActionResult<PersonResponse>> GetByFontysId(string id)
        {
            try
            {
                return Ok(await _service.GetSingleByFontysId(id));
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
