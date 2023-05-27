using api_gateway.Helper;
using api_gateway.Models.RequestModels.Location;
using api_gateway.Models.ServiceModels.Location;
using Flurl.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
    /// <summary>
    /// The location controller handles all of the web-friendly endpoints that consume the location microservice
    /// Controller documentation should be handled automatically via swagger.
    /// For more information visit the generated swagger documentation.
    /// </summary>
    [Produces("application/json")]
    [Route("api/locations")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class LocationController : ControllerBase
    {
        #region Get methods.
        [HttpGet("rooms")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ICollection<Room>>> GetRooms([FromHeader] string Authorization)
        {
            var user = User.Identity;
            IFlurlResponse flurlResponse = await $"{Constants.LocationApiUrl}/api/rooms".WithHeaders( new { Accept = "text/plain", Authorization }).GetAsync();
            var response = flurlResponse.GetResponse("Er kunnen geen ruimtes gevonden worden");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return new ObjectResult(response.Message) { StatusCode = (int)response.StatusCode };
            }

            ICollection<Room> responseModels = await flurlResponse.GetJsonAsync<ICollection<Room>>();
            return Ok(responseModels);
        }

        [HttpGet("cities")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ICollection<City>>> GetCities([FromHeader] string Authorization)
        {
            IFlurlResponse flurlResponse = await $"{Constants.LocationApiUrl}/api/cities".WithHeaders(new { Accept = "text/plain", Authorization }).GetAsync();
            var response = flurlResponse.GetResponse("Er kunnen geen steden gevonden worden");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return new ObjectResult(response.Message) { StatusCode = (int)response.StatusCode };
            }

            ICollection<City> responseModels = await flurlResponse.GetJsonAsync<ICollection<City>>();
            return Ok(responseModels);
        }

        [HttpGet("buildings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ICollection<Building>>> GetBuildings([FromHeader] string Authorization)
        {
            IFlurlResponse flurlResponse = await $"{Constants.LocationApiUrl}/api/buildings".WithHeaders(new { Accept = "text/plain", Authorization }).GetAsync();
            var response = flurlResponse.GetResponse("Er kunnen geen gebouwen gevonden worden");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return new ObjectResult(response.Message) { StatusCode = (int)response.StatusCode };
            }

            ICollection<Building> responseModels = await flurlResponse.GetJsonAsync<ICollection<Building>>();
            return Ok(responseModels);
        }

        [HttpGet("buildings/city/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ICollection<Building>>> GetAllBuildingsByCity(Guid id, [FromHeader] string Authorization)
        {
            IFlurlResponse flurlResponse = await $"{Constants.LocationApiUrl}/api/buildings/city/{id}".WithHeaders(new { Accept = "text/plain", Authorization }).GetAsync();
            var response = flurlResponse.GetResponse("Dit gebouw kan niet gevonden worden");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return new ObjectResult(response.Message) { StatusCode = (int)response.StatusCode };
            }

            ICollection<Building> responseModels = await flurlResponse.GetJsonAsync<ICollection<Building>>();
            return Ok(responseModels);
        }

        [HttpGet("rooms/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Room>> GetRoomById(Guid id, [FromHeader] string Authorization)
        {
            IFlurlResponse flurlResponse = await $"{Constants.LocationApiUrl}/api/rooms/{id}".WithHeaders(new { Accept = "text/plain", Authorization }).GetAsync();
            var response = flurlResponse.GetResponse("Deze ruimte kan niet gevonden worden");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return new ObjectResult(response.Message) { StatusCode = (int)response.StatusCode };
            }

            Room responseModel = await flurlResponse.GetJsonAsync<Room>();
            return Ok(responseModel);

        }

        [HttpGet("cities/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<City>> GetCityById(Guid id, [FromHeader] string Authorization)
        {
            IFlurlResponse flurlResponse = await $"{Constants.LocationApiUrl}/api/cities/{id}".WithHeaders(new { Accept = "text/plain", Authorization }).GetAsync();
            var response = flurlResponse.GetResponse("Deze stad kan niet gevonden worden");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return new ObjectResult(response.Message) { StatusCode = (int)response.StatusCode };
            }

            City responseModel = await flurlResponse.GetJsonAsync<City>();
            return Ok(responseModel);

        }

        [HttpGet("buildings/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Building>> GetBuildingById(Guid id, [FromHeader] string Authorization)
        {
            IFlurlResponse flurlResponse = await $"{Constants.LocationApiUrl}/api/buildings/{id}".WithHeaders(new { Accept = "text/plain", Authorization }).GetAsync();
            var response = flurlResponse.GetResponse("Dit gebouw kan niet gevonden worden");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return new ObjectResult(response.Message) { StatusCode = (int)response.StatusCode };
            }

            Building responseModel = await flurlResponse.GetJsonAsync<Building>();
            return Ok(responseModel);
        }

        [HttpGet("rooms/name/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Room>> GetRoomByName(string name, [FromHeader] string Authorization)
        {
            IFlurlResponse flurlResponse = await $"{Constants.LocationApiUrl}/api/rooms/name/{name}".WithHeaders(new { Accept = "text/plain", Authorization }).GetAsync();
            var response = flurlResponse.GetResponse("Deze kamer kan niet gevonden worden");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return new ObjectResult(response.Message) { StatusCode = (int)response.StatusCode };
            }

            Room responseModel = await flurlResponse.GetJsonAsync<Room>();
            return Ok(responseModel);
        }

        [HttpGet("cities/name/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<City>> GetCityByName(string name, [FromHeader] string Authorization)
        {
            IFlurlResponse flurlResponse = await $"{Constants.LocationApiUrl}/api/cities/name/{name}".WithHeaders(new { Accept = "text/plain", Authorization }).GetAsync();
            var response = flurlResponse.GetResponse("Deze stad kan niet gevonden worden");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return new ObjectResult(response.Message) { StatusCode = (int)response.StatusCode };
            }

            City responseModel = await flurlResponse.GetJsonAsync<City>();
            return Ok(responseModel);
        }

        [HttpGet("buildings/name/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Building>> GetBuildingByName(string name, [FromHeader] string Authorization)
        {
            IFlurlResponse flurlResponse = await $"{Constants.LocationApiUrl}/api/buildings/name/{name}".WithHeaders(new { Accept = "text/plain", Authorization }).GetAsync();
            var response = flurlResponse.GetResponse("Dit gebouw kan niet gevonden worden");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return new ObjectResult(response.Message) { StatusCode = (int)response.StatusCode };
            }

            Building responseModel = await flurlResponse.GetJsonAsync<Building>();
            return Ok(responseModel);
        }
        #endregion

        #region Post methods
        [HttpPost("rooms")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Room>> PostRoom(RoomRequestModel request, [FromHeader] string Authorization)
        {
            ObjectResult buildingResponse = await BuildingExists(request.BuildingId.ToString(), Authorization);
            if (buildingResponse.StatusCode != 200)
            {
                return buildingResponse;
            }

            //post room
            IFlurlResponse flurlResponse = await $"{Constants.LocationApiUrl}/api/rooms".WithHeaders(new { Accept = "text/plain", Authorization }).PostJsonAsync(request);
            var response = flurlResponse.GetResponse();

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return new ObjectResult(response.Message) { StatusCode = (int)response.StatusCode };
            }

            Room responseModel = await flurlResponse.GetJsonAsync<Room>();
            return CreatedAtAction("PostRoom", responseModel);
        }

        [HttpPost("cities")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<City>> PostCity(CityRequestModel request, [FromHeader] string Authorization)
        {
            IFlurlResponse flurlResponse = await $"{Constants.LocationApiUrl}/api/cities".WithHeaders(new { Accept = "text/plain", Authorization }).PostJsonAsync(request);
            var response = flurlResponse.GetResponse();

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return new ObjectResult(response.Message) { StatusCode = (int)response.StatusCode };
            }
            else if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return new ObjectResult(response.Message) { StatusCode = (int)response.StatusCode };
            }
            City responseModel = await flurlResponse.GetJsonAsync<City>();
            return CreatedAtAction("PostCity", responseModel);
        }

        [HttpPost("buildings")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Building>> PostBuilding(BuildingRequestModel request, [FromHeader] string Authorization)
        {
            ObjectResult cityResponse = await CityExists(request.Address.CityId.ToString(), Authorization);
            if (cityResponse.StatusCode != 200)
            {
                return cityResponse;
            }

            //post building
            IFlurlResponse flurlResponse = await $"{Constants.LocationApiUrl}/api/buildings".WithHeaders(new { Accept = "text/plain", Authorization }).PostJsonAsync(request);
            var response = flurlResponse.GetResponse();

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return new ObjectResult(response.Message) { StatusCode = (int)response.StatusCode };
            }

            Building responseModel = await flurlResponse.GetJsonAsync<Building>();
            return CreatedAtAction("PostBuilding", responseModel);
        }
        #endregion

        #region Put methods.
        [HttpPut("buildings/{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Building>> PutBuilding(BuildingRequestModel request, Guid id, [FromHeader] string Authorization)
        {
            ObjectResult cityResponse = await CityExists(request.Address.CityId.ToString(), Authorization);
            if (cityResponse.StatusCode != 200)
            {
                return cityResponse;
            }

            //put building
            IFlurlResponse flurlResponse = await $"{Constants.LocationApiUrl}/api/buildings/{id}".WithHeaders(new { Accept = "text/plain", Authorization }).PutJsonAsync(request);
            var response = flurlResponse.GetResponse();

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return new ObjectResult(response.Message) { StatusCode = (int)response.StatusCode };
            }

            Building responseModel = await flurlResponse.GetJsonAsync<Building>();
            return CreatedAtAction("PutBuilding", responseModel);

        }

        [HttpPut("cities/{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<City>> PutCity(CityRequestModel request, Guid id, [FromHeader] string Authorization)
        {
            IFlurlResponse flurlResponse = await $"{Constants.LocationApiUrl}/api/cities/{id}".WithHeaders(new { Accept = "text/plain", Authorization }).PutJsonAsync(request);
            var response = flurlResponse.GetResponse();

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return new ObjectResult(response.Message) { StatusCode = (int)response.StatusCode };
            }

            City responseModel = await flurlResponse.GetJsonAsync<City>();
            return CreatedAtAction("PutCity", responseModel);

        }

        [HttpPut("rooms/{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Room>> PutRoom(RoomRequestModel request, Guid id, [FromHeader] string Authorization)
        {
            ObjectResult buildingResponse = await BuildingExists(request.BuildingId.ToString(), Authorization);
            if (buildingResponse.StatusCode != 200)
            {
                return buildingResponse;
            }

            //put room
            IFlurlResponse flurlResponse = await $"{Constants.LocationApiUrl}/api/rooms/{id}".WithHeaders(new { Accept = "text/plain", Authorization }).PutJsonAsync(request);
            //var response = flurlResponse.GetResponse();
            if (flurlResponse.StatusCode != 200)
            {
                Task<string> result = flurlResponse.GetStringAsync();
                return new ObjectResult(result.Result) { StatusCode = flurlResponse.StatusCode };
            }

            Room responseModel = await flurlResponse.GetJsonAsync<Room>();
            return CreatedAtAction("PutRoom", responseModel);

        }
        #endregion

        #region Delete methods.
        [HttpDelete("buildings/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteBuilding(Guid id, [FromHeader] string Authorization)
        {
            IFlurlResponse response = await $"{Constants.LocationApiUrl}/api/buildings/{id}".WithHeaders(new { Accept = "text/plain", Authorization }).DeleteAsync();
            var buildingResponse = response.GetResponse("Het meegegeven gebouw bestaat niet");

            if (buildingResponse.StatusCode != HttpStatusCode.NoContent) // Service returns 204 on delete.
            {
                return new ObjectResult(buildingResponse.Message) { StatusCode = (int)buildingResponse.StatusCode };
            }

            return new ObjectResult(buildingResponse.Message) { StatusCode = (int)buildingResponse.StatusCode };
        }


        [HttpDelete("cities/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteCity(Guid id, [FromHeader] string Authorization)
        {
            IFlurlResponse response = await $"{Constants.LocationApiUrl}/api/cities/{id}".WithHeaders(new { Accept = "text/plain", Authorization }).DeleteAsync();
            var cityResponse = response.GetResponse("De meegegeven stad bestaat niet");

            if (cityResponse.StatusCode != HttpStatusCode.NoContent) // Service returns 204 on delete.
            {
                return new ObjectResult(cityResponse.Message) { StatusCode = (int)cityResponse.StatusCode };
            }

            return new ObjectResult(cityResponse.Message) { StatusCode = (int)cityResponse.StatusCode };
        }

        [HttpDelete("rooms/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteRoom(Guid id, [FromHeader] string Authorization)
        {
            IFlurlResponse response = await $"{Constants.LocationApiUrl}/api/rooms/{id}".WithHeaders(new { Accept = "text/plain", Authorization }).DeleteAsync();
            var roomResponse = response.GetResponse("De meegegeven ruimte bestaat niet");

            if (roomResponse.StatusCode != HttpStatusCode.NoContent) // Service returns 204 on delete.
            {
                return new ObjectResult(roomResponse.Message) { StatusCode = (int)roomResponse.StatusCode };
            }

            return new ObjectResult(roomResponse.Message) { StatusCode = (int)roomResponse.StatusCode };
        }
        #endregion

        #region Helper methods.
        private async Task<ObjectResult> CityExists(string id, string Authorization)
        {
            IFlurlResponse flurlCityResponse = await $"{Constants.LocationApiUrl}/api/cities/{id}".WithHeaders(new { Accept = "text/plain", Authorization }).GetAsync();
            var cityResponse = flurlCityResponse.GetResponse("De meegegeven stad bestaat niet");

            if (cityResponse.StatusCode != HttpStatusCode.OK)
            {
                return new ObjectResult(cityResponse.Message) { StatusCode = (int)cityResponse.StatusCode };
            }

            return new ObjectResult(cityResponse.Message) { StatusCode = (int)cityResponse.StatusCode };
        }

        private async Task<ObjectResult> BuildingExists(string id, string Authorization)
        {
            IFlurlResponse flurlBuildingResponse = await $"{Constants.LocationApiUrl}/api/buildings/{id}".WithHeaders(new { Accept = "text/plain", Authorization }).GetAsync();
            var buildingResponse = flurlBuildingResponse.GetResponse("Het meegegeven gebouw bestaat niet");

            if (buildingResponse.StatusCode != HttpStatusCode.OK)
            {
                return new ObjectResult(buildingResponse.Message) { StatusCode = (int)buildingResponse.StatusCode };
            }

            return new ObjectResult(buildingResponse.Message) { StatusCode = (int)buildingResponse.StatusCode };
        }
        #endregion
    }
}
