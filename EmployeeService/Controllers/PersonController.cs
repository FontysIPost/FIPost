using System.Threading.Tasks;
using EmployeeService.Helpers;
using EmployeeService.Models.DTO_s;
using EmployeeService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers;

[Route("api/persons")]
[ApiController]
[Authorize]
public class PersonController : Controller
{
    private readonly IPersonService _service;

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