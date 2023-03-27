using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace personeel_service.Models.DTO_s
{
    public class PersonResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string FontysId { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
    }
}
