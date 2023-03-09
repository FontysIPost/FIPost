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

namespace authentication_service.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly DataContext db;
        TokenController TC = new TokenController();
        public AuthenticationController(DataContext db)
        {
            this.db = db;
        }

        [HttpPost]
        [Route("/api/[controller]/login")]
        public object login([FromBody] Person p)
        {
            Person person = (from Person in db.Person
                             where Person.Email == p.Email && Person.Password == p.Password
                             select Person).FirstOrDefault();

            if (person == null)
            {
                return Unauthorized();
            }
            else
            {
                return TC.GenerateToken(p.Email, Convert.ToString(person.Role));
            }
        }

        [Route("/api/[controller]/register")]
        [HttpPost]
        public object register()
        {
            var x = TC.GenerateToken(null, null);
            return x;
        }

        [HttpGet]
        [Route("/api/[controller]/auth")]
        [Authorize]
        public string authorize([FromHeader] string Authorization)
        {
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
