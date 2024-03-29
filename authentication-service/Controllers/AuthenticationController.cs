﻿using authentication_service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace authentication_service.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly DataContext db;
        readonly TokenController TC = new();
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

            return person == null ? Unauthorized() : TC.GenerateToken(p.Email, Convert.ToString(person.Role));
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
            string email = "";
            int role = 0;
            foreach (Claim c in jwtSecurityToken.Claims)
            {
                if (c.Type == "email")
                {
                    email = c.Value;
                }
                else if (ClaimTypes.Role == c.Type)
                {
                    role = Convert.ToInt32(c.Value);
                }
            }
            UserResponse userResponse = new UserResponse(email, role);
            string jSonObject = JsonConvert.SerializeObject(userResponse);
            return jSonObject;
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
