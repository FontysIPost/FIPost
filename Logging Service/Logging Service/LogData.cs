using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logging_Service
{
    public class LogData
    {
        public int Id { get; set; }
        public string LogLevel { get; set; }
        public DateTime Timestamp { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public string FontysID { get; set; }

    }
}
