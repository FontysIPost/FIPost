using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Logging_Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoggingController : ControllerBase
    {
        private readonly LoggingService _loggingService;

        public LoggingController(IConfiguration configuration)
        {
            _loggingService = new LoggingService(configuration);
        }

        [HttpPost("error")]
        public IActionResult LogError([FromBody] LogData logData)
        {
            // Log the error
            _loggingService.LogDataToDatabase(logData);

            // Return a success response
            return Ok();
        }

        [HttpPost("warning")]
        public IActionResult LogWarning([FromBody] LogData logData)
        {
            // Log the warning
            _loggingService.LogDataToDatabase(logData);

            // Return a success response
            return Ok();
        }

        [HttpPost("info")]
        public IActionResult LogInfo([FromBody] LogData logData)
        {
            // Log the informational message
            _loggingService.LogDataToDatabase(logData);

            // Return a success response
            return Ok();
        }
    }
}
