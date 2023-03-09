using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace api_gateway.Controllers
{
    [ApiController]
    public class AuthenticationController
    {
        /*
        TokenController TC = new TokenController();

        [Route("/[controller]/register")]
        [HttpPost]
        public object register()
        {
            var x = TC.GenerateToken();
            return x;
        }*/

        [HttpGet]
        [Route("/[controller]/auth")]
        [Authorize]
        public string authorize()
        {
            //var user = User.Identity;
            return "huts";
        }
    }
}
