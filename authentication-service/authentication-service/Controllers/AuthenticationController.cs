using authentication_service.Data;
using authentication_service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace authentication_service.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly DataContext db;
        TokenController TC = new TokenController();


        public static async Task LogToMicroservice(string logMessage,string url )
        {
            using (var httpClient = new HttpClient())
            {
                var payload = new StringContent(logMessage, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(url, payload);

                // Check the response status code
                if (!response.IsSuccessStatusCode)
                {
                    // Log an error message if the request failed
                    Console.WriteLine("Failed to log to microservice");
                }
            }
        }









        public AuthenticationController(DataContext db, ILogger<AuthenticationController> logger)
        {
            this.db = db;
            _logger = logger;
        }

        [HttpPost]
        [Route("/api/[controller]/login")]
        public async Task<object> loginAsync([FromBody] Person p)
        {
            Person person = (from Person in db.Person
                             where Person.Email == p.Email && Person.Password == p.Password
                             select Person).FirstOrDefault();

            if (person == null)
            {
                _logger.LogWarning("Invalid login attempt for email: {0}", p.Email);
                return Unauthorized();
            }
            else
            {
                _logger.LogInformation("User logged in: {0}", p.Email);
                await LogToMicroservice($"User logged in: {p.Email}","");
                return TC.GenerateToken(p.Email, Convert.ToString(person.Role));
            }
        }

        [Route("/api/[controller]/register")]
        [HttpPost]
        public object register()
        {
            _logger.LogInformation("User registration attempt");
            var x = TC.GenerateToken(null, null);
            return x;
        }

        [HttpGet]
        [Route("/api/[controller]/auth")]
        [Authorize]
        public string authorize([FromHeader] string Authorization)
        {
            _logger.LogInformation("User authorized");
            string[] token = Authorization.Split(' ');
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token[1]);
            foreach (Claim c in jwtSecurityToken.Claims)
            {
                if (c.Type == "email")
                {
                    return c.Value;
                }
            }
            return "";
        }

        [HttpGet]
        [Route("/api/[controller]/singleUser")]
        [Authorize]
        public Person GetUser([FromHeader] string Authorization)
        {
            _logger.LogInformation("User details requested");
            string[] token = Authorization.Split(' ');
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token[1]);
            string email = "";
            foreach (Claim c in jwtSecurityToken.Claims)
            {
                if (c.Type == "email")
                {
                    email = c.Value;
                }
            }
            Person p = (from Person in db.Person
                        where Person.Email == email
                        select Person).FirstOrDefault();
            return p;
        }
    }
}
