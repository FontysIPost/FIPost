namespace EmployeeService.Models;

public class Person
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
    public string Password { get; set; }
    public int Role { get; set; }
    public string FontysId { get; }
}