using Microsoft.EntityFrameworkCore;
using personeel_service.Models;

namespace personeel_service.Database.Contexts
{
    public class PersonServiceContext : DbContext
    {
        public PersonServiceContext(DbContextOptions<PersonServiceContext> options)
            : base(options)
        {
        }

        public DbSet<Person> Person { get; set; }

    }
}
