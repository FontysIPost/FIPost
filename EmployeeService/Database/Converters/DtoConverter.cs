using personeel_service.Database.Converters;
using personeel_service.Database.DTO_s;
using personeel_service.Models;
using personeel_service.Models.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace personeel_service.Database.Converters
{
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
            List<PersonResponse> responseDtos = new List<PersonResponse>();

            foreach (Person pkg in persons)
            {
                responseDtos.Add(ModelToDto(pkg));
            }

            return responseDtos;
        }

    }
}
