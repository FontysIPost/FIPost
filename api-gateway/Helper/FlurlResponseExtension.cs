using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace api_gateway.Helper
{
    public static class FlurlResponseExtension
    {
        public static ErrorResponse GetResponse(this IFlurlResponse flurlResponse, string notFoundMessage = "Niet gevonden")
        {
            switch (flurlResponse.StatusCode)
            {
                case 201:
                    return new ErrorResponse(HttpStatusCode.Created, "");
                case 400:
                    return new ErrorResponse(HttpStatusCode.BadRequest, "Er ging iets mis. Probeer het later opnieuw");
                case 404:
                    return new ErrorResponse(HttpStatusCode.NotFound, notFoundMessage);
                case 409:
                    return new ErrorResponse(HttpStatusCode.Conflict, "Het item dat u probeert toe te voegen/updaten bestaat al");
                case 500:
                    return new ErrorResponse(HttpStatusCode.InternalServerError, "Er ging iets mis. Probeer het later opnieuw");
                default:
                    return new ErrorResponse(HttpStatusCode.OK, "");
            }
        }
    }
}
