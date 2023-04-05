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
        public string Message { get; set; }
        public string Exception { get; set; }
    }
}
