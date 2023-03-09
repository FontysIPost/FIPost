using api_gateway.Helper;
using api_gateway.Models.ServiceModels;
using Flurl.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace api_gateway.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class PersonController : ControllerBase
    {
        #region Get methods.
        [HttpGet("persons")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ICollection<PersonServiceModel>>> GetPersons()
        {
            IFlurlResponse flurlResponse = await $"{Constants.PersonApiUrl}/api/persons".GetAsync();

            var response = flurlResponse.GetResponse("Er zijn geen personen gevonden.");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return new ObjectResult(response.Message) { StatusCode = (int)response.StatusCode };
            }

            ICollection<PersonServiceModel> responseModels = await flurlResponse.GetJsonAsync<ICollection<PersonServiceModel>>();
            return Ok(responseModels);
        }

        [HttpGet("persons/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PersonServiceModel>> GetPersonById(string id)
        {
            IFlurlResponse flurlResponse = await $"{Constants.PersonApiUrl}/api/persons/{id}".GetAsync();

            var response = flurlResponse.GetResponse("De persoon die u probeert op te zoeken kan niet gevonden worden.");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return new ObjectResult(response.Message) { StatusCode = (int)response.StatusCode };
            }

            PersonServiceModel responseModel = await flurlResponse.GetJsonAsync<PersonServiceModel>();
            return Ok(responseModel);
        }

        [HttpGet("persons/getbyfontysid/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PersonServiceModel>> getByFontysId(string id)
        {
            IFlurlResponse flurlResponse = await $"{Constants.PersonApiUrl}/api/persons/getbyfontysid/{id}".GetAsync();

            var response = flurlResponse.GetResponse("De persoon die u probeert op te zoeken kan niet gevonden worden.");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return new ObjectResult(response.Message) { StatusCode = (int)response.StatusCode };
            }

            PersonServiceModel responseModel = await flurlResponse.GetJsonAsync<PersonServiceModel>();
            return Ok(responseModel);
        }
        #endregion

        [HttpGet("health")]
        public ActionResult Health()
        {
            return Ok();
        }
    }
}
