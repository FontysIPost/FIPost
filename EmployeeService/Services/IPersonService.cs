using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeService.Models.DTO_s;

namespace EmployeeService.Services;

public interface IPersonService
{
    Task<List<PersonResponse>> GetAllAsync();

    // List<Person> GetAll();
    // Person GetById(string id);
    Task<PersonResponse> GetByIdAsync(string id);
    Task<PersonResponse> GetSingleByFontysId(string fontysId);
}