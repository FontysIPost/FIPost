using System.Collections.Generic;
using EmployeeService.Models;
using EmployeeService.Models.DTO_s;

namespace EmployeeService.Database.Converters;

public class DtoConverter : IDtoConverter<Person, PersonRequest, PersonResponse>
{
    public Person DtoToModel(PersonRequest dto)
    {
        return new Person
        {
            Id = dto.Id,
            Name = dto.Name,
            Email = dto.Email
        };
    }

    public PersonResponse ModelToDto(Person person)
    {
        return new PersonResponse
        {
            Id = person.Id,
            Name = person.Name,
            Email = person.Email,
            FontysId = person.FontysId
        };
    }

    public List<PersonResponse> ModelToDto(List<Person> persons)
    {
        var responseDtos = new List<PersonResponse>();

        foreach (var pkg in persons) responseDtos.Add(ModelToDto(pkg));

        return responseDtos;
    }
}