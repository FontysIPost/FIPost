using System.ComponentModel.DataAnnotations;

namespace EmployeeService.Models.DTO_s;

public class PersonRequest
{
    [Required] public int Id { get; set; }

    [Required] public string Name { get; set; }

    [Required] public string Email { get; set; }

    [Required] public string Password { get; set; }
    //public int Role { get; set; }
}