using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;


namespace Logging_Service
{
    public class LoggingService
    {
        private readonly MyDbContext _dbContext;

        public LoggingService(IConfiguration configuration)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Default"));

            _dbContext = new MyDbContext(optionsBuilder.Options);
        }

        public void LogDataToDatabase(LogData logData)
        {
            // Create a new LogEntry object to store in the database
            var logEntry = new LogData
            {
                LogLevel = logData.LogLevel,
                Timestamp = DateTime.Now,
                Email = logData.Email,
                Password = logData.Password,
                Role = logData.Role,
                FontysID = logData.FontysID,
            };

            // Add the LogEntry to the database and save changes
            _dbContext.LogEntries.Add(logEntry);
            _dbContext.SaveChanges();
        }

    }
}
