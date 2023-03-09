using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace authentication_service.Models
{
    public class UserDTO
    {
        public string email { get; set; }
        public string token { get; set; }
    }
}
