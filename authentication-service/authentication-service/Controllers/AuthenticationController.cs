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
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace authentication_service.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly DataContext db;
        TokenController TC = new TokenController();


        public class LogData
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public int Role { get; set; }
            public string FontysID { get; set; }

            public string LogLevel { get; set; }
        }

        public static async Task LogToMicroservice(string Email, string Password,int Role, string FontysID, string url )
        {
            using (var httpClient = new HttpClient())
            {


                // Create a LogData object from your log message
                var logData = new LogData
                {
                    Email = Email,
                    Role = Role,
                    FontysID = FontysID,
                    Password = Password,
                    LogLevel = "UserInfo"
                };

                var jsonPayload = JsonConvert.SerializeObject(logData);
                var payload = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
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
                await LogToMicroservice(p.Email,p.Password,p.Role,p.FontysId,"https://localhost:44331/Logging/info");
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
