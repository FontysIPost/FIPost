using api_gateway.Helper;
using api_gateway.Models.Converters;
using api_gateway.Models.RequestModels;
using api_gateway.Models.ResponseModels;
using api_gateway.Models.ServiceModels;
using api_gateway.Models.ServiceModels.Location;
using Flurl.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api_gateway.Controllers
{
    /// <summary>
    /// The package controller handles all of the web-friendly endpoints that consume the package resource.
    /// Controller documentation should be handled automatically via swagger.
    /// For more information visit the generated swagger documentation.
    /// </summary>
    [Produces("application/json")]
    [Route("api/packages")]
    [ApiController]
    [Authorize]

    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class PackageController : ControllerBase
    {

        #region Get methods.
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ICollection<PackageResponseModel>>> Get()
        {
            IFlurlResponse packageResponse = await $"{Constants.PackageApiUrl}/api/packages".GetAsync();
            //var errPackageResponse = packageResponse.GetResponse("Er zijn nog geen pakketten");
            if (packageResponse.StatusCode != 200)
            {
                Task<string> result = packageResponse.GetStringAsync();
                return new ObjectResult(result.Result) { StatusCode = packageResponse.StatusCode };
            }

            ICollection<PackageServiceModel> allPackageServiceModels = await packageResponse.GetJsonAsync<ICollection<PackageServiceModel>>();
            ICollection<PackageResponseModel> allPackages = ServiceToResponseModelConverter.ConvertPackages(allPackageServiceModels, await GetAllPersons(), await GetAllRooms());

            return Ok(allPackages);

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PackageResponseModel>> GetById(Guid id)
        {
            IFlurlResponse packageResponse = await $"{Constants.PackageApiUrl}/api/packages/{id}".GetAsync();

            var errPackageResponse = packageResponse.GetResponse();

            if (errPackageResponse.StatusCode != HttpStatusCode.OK)
            {
                return new ObjectResult(errPackageResponse.Message) { StatusCode = (int)errPackageResponse.StatusCode };
            }
            PackageServiceModel packageModel = await packageResponse.GetJsonAsync<PackageServiceModel>();


            PackageResponseModel responseModel = ServiceToResponseModelConverter.ConvertPackage(packageModel, await GetAllPersons(), await GetAllRooms());
            return Ok(responseModel);

        }
        #endregion

        #region Post methods.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PackageResponseModel>> PostPackage(PackageRequestModel request)
        {
            ObjectResult personResponse = await PersonExists(request.ReceiverId);
            if (personResponse.StatusCode != 200)
            {
                return personResponse;
            }

            ObjectResult collectionPointResponse = await CollectionPointExists(request.CollectionPointId);
            if (collectionPointResponse.StatusCode != 200)
            {
                return collectionPointResponse;
            }

            //Post package
            IFlurlResponse flurlPostResponse = await $"{Constants.PackageApiUrl}/api/packages".PostJsonAsync(request);
            var postResponse = flurlPostResponse.GetResponse();

            if (postResponse.StatusCode != HttpStatusCode.OK)
            {
                return new ObjectResult(postResponse.Message) { StatusCode = (int)postResponse.StatusCode };
            }

            PackageServiceModel model = await flurlPostResponse.GetJsonAsync<PackageServiceModel>();
            PackageResponseModel responseModel = ServiceToResponseModelConverter.ConvertPackage(model, await GetAllPersons(), await GetAllRooms());

            //send registration mail
            // await "https://mailservice20210603092014.azurewebsites.net/api/TrackAndTraceMail?code=bTMCXQQGWaQycYLfbP/Vq749V03PPkSbmwRyfQlBXlVQq9WZyR4U7Q==".PostJsonAsync(responseModel);

            return CreatedAtAction("PostPackage", responseModel);
        }

        [HttpPost("tickets")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TicketResponseModel>> PostTicket(TicketRequestModel request)
        {
            ObjectResult personResponse = await PersonExists(request.CompletedByPersonId);
            if (personResponse.StatusCode != 200)
            {
                return personResponse;
            }

            ObjectResult collectionPointResponse = await CollectionPointExists(request.LocationId);
            if (collectionPointResponse.StatusCode != 200)
            {
                return collectionPointResponse;
            }

            //Post ticket
            IFlurlResponse flurlPostResponse = await $"{Constants.PackageApiUrl}/api/tickets".PostJsonAsync(request);
            var postResponse = flurlPostResponse.GetResponse();

            if (postResponse.StatusCode != HttpStatusCode.OK)
            {
                return new ObjectResult(postResponse.Message) { StatusCode = (int)postResponse.StatusCode };
            }

            TicketServiceModel model = await flurlPostResponse.GetJsonAsync<TicketServiceModel>();
            TicketResponseModel responseModel = ServiceToResponseModelConverter.ConvertTicket(model);

            //check if package is finished and then send arrival email
            IFlurlResponse flurlPackageResponse = await $"{Constants.PackageApiUrl}/api/packages/{request.PackageId}".GetAsync();

            PackageServiceModel pkgService = await flurlPackageResponse.GetJsonAsync<PackageServiceModel>();
            PackageResponseModel pkg = ServiceToResponseModelConverter.ConvertPackage(pkgService, await GetAllPersons(), await GetAllRooms());

            Console.WriteLine("package route finished is " + pkg.RouteFinished);

            if (pkg.RouteFinished)
            {
                //send email
                await "https://mailservice20210603092014.azurewebsites.net/api/ArrivalMail?code=gYOUs9FO7WwwNXz2eSGtZM0AFxQl/RQvOJ4RF0uotwYLe7l/AIGGKg==".PostJsonAsync(pkg);
            }

            return CreatedAtAction("PostTicket", responseModel);
        }
        #endregion

        #region Put methods.
        // PUT api/packages/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PackageResponseModel>> PutPackage(Guid id, PackageRequestModel request)
        {
            ObjectResult personResponse = await PersonExists(request.ReceiverId);
            if (personResponse.StatusCode != 200)
            {
                return personResponse;
            }

            ObjectResult collectionPointResponse = await CollectionPointExists(request.CollectionPointId);
            if (collectionPointResponse.StatusCode != 200)
            {
                return collectionPointResponse;
            }

            IFlurlResponse flurlPutResponse = await $"{Constants.PackageApiUrl}/api/packages/{id}".PutJsonAsync(request);
            var putResponse = flurlPutResponse.GetResponse();

            if (putResponse.StatusCode != HttpStatusCode.OK)
            {
                return new ObjectResult(putResponse.Message) { StatusCode = (int)putResponse.StatusCode };
            }

            PackageServiceModel model = await flurlPutResponse.GetJsonAsync<PackageServiceModel>();
            PackageResponseModel responseModel = ServiceToResponseModelConverter.ConvertPackage(model);
            return CreatedAtAction("PutPackage", responseModel);
        }
        #endregion

        #region Delete methods.
        // DELETE api/packages/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(Guid id)
        {
            IFlurlResponse response = await $"{Constants.PackageApiUrl}/api/packages/{id}".DeleteAsync();
            var deleteResponse = response.GetResponse("Het meegegeven pakket bestaat niet");

            if (deleteResponse.StatusCode != HttpStatusCode.NoContent) // Service returns 204 on delete.
            {
                return new ObjectResult(deleteResponse.Message) { StatusCode = (int)deleteResponse.StatusCode };
            }

            return new ObjectResult(deleteResponse.Message) { StatusCode = (int)deleteResponse.StatusCode };
        }
        #endregion

        #region Helper methods.
        private async Task<ObjectResult> PersonExists(string id)
        {
            IFlurlResponse flurlPersonResponse = await $"{Constants.PersonApiUrl}/api/persons/{id}".GetAsync();
            var personResponse = flurlPersonResponse.GetResponse("De meegegeven ontvanger bestaat niet");

            if (personResponse.StatusCode != HttpStatusCode.OK)
            {
                return new ObjectResult(personResponse.Message) { StatusCode = (int)personResponse.StatusCode };
            }

            return new ObjectResult(personResponse.Message) { StatusCode = (int)personResponse.StatusCode };
        }

        private async Task<ObjectResult> CollectionPointExists(Guid id)
        {
            IFlurlResponse flurlCollectionPointResponse = await $"{Constants.LocationApiUrl}/api/rooms/{id}".GetAsync();
            var collectionPointResponse = flurlCollectionPointResponse.GetResponse("De meegegeven ruimte bestaat niet");

            if (collectionPointResponse.StatusCode != HttpStatusCode.OK)
            {
                return new ObjectResult(collectionPointResponse.Message) { StatusCode = (int)collectionPointResponse.StatusCode };
            }

            return new ObjectResult(collectionPointResponse.Message) { StatusCode = (int)collectionPointResponse.StatusCode };
        }

        private async Task<ICollection<Room>> GetAllRooms()
        {
            IFlurlResponse locationResponse = await $"{Constants.LocationApiUrl}/api/rooms".GetAsync();
            var errLocationResponse = locationResponse.GetResponse();
            ICollection<Room> allRooms = null;

            if (errLocationResponse.StatusCode == HttpStatusCode.OK)
            {
                allRooms = await locationResponse.GetJsonAsync<ICollection<Room>>();
            }
            return allRooms;
        }

        private async Task<ICollection<PersonServiceModel>> GetAllPersons()
        {
            IFlurlResponse personResponse = await $"{Constants.PersonApiUrl}/api/persons".GetAsync();
            var errPersonResponse = personResponse.GetResponse();
            ICollection<PersonServiceModel> allPersonServiceModels = null;

            if (errPersonResponse.StatusCode == HttpStatusCode.OK)
            {
                allPersonServiceModels = await personResponse.GetJsonAsync<ICollection<PersonServiceModel>>();
            }

            return allPersonServiceModels;
        }
        #endregion
    }
}
