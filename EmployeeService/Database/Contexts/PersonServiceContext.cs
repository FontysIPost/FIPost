using EmployeeService.Models;

namespace EmployeeService.Database.Contexts;

public class PersonServiceContext : DbContext
{
    public PersonServiceContext(DbContextOptions<PersonServiceContext> options)
        : base(options)
    {
    }

    public DbSet<Person> Person { get; set; }
}