using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Logging_Service
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public DbSet<LogData> LogEntries { get; set; }
        public DbSet<LocatiesData> LocatiesEntries { get; set; }
        public DbSet<PakketjeData> PakketjesEntries { get; set; }
    }
}
