using Microsoft.EntityFrameworkCore;
using personeel_service.Database.Contexts;
using personeel_service.Database.Converters;
using personeel_service.Database.DTO_s;
using personeel_service.Helpers;
using personeel_service.Models;
using personeel_service.Models.DTO_s;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace personeel_service.Services
{
    public class PersonService : IPersonService
    {
        /*  private static readonly IEnumerable<Person> persons = new List<Person>()
          {
              new Person("1", "Jaap van der Meer", "fipostspamn@hotmail.com"),
              new Person("2", "Sverre van Gompel", "fipostspamn@hotmail.com"),
              new Person("3", "Kevin Wieling", "fipostspamn@hotmail.com"),
              new Person("4", "Robin de Witte", "fipostspamn@hotmail.com"),
              new Person("5", "Sjors Scholten", "fipostspamn@hotmail.com"),
              new Person("6", "Aron Heesakkers", "fipostspamn@hotmail.com")
          };*/

        private readonly PersonServiceContext _context;
        private readonly IDtoConverter<Person, PersonRequest, PersonResponse> _converter;

        public PersonService(PersonServiceContext context, IDtoConverter<Person, PersonRequest, PersonResponse> converter)
        {
            _context = context;
            _converter = converter;
        }



        public async Task<List<PersonResponse>> GetAllAsync()
        {
            //return persons.ToList();

            List<PersonResponse> persons = _converter.ModelToDto(await _context.Person.ToListAsync());


            return persons;

        }

        public async Task<PersonResponse> GetByIdAsync(string id)
        {

            /*var person = persons.FirstOrDefault(p => p.Id == id);

            if (person == null)
            {
                throw new NotFoundException($"Person with id {id} not found.");
            }

            return person;*/


            PersonResponse person = _converter.ModelToDto(await _context.Person.FirstOrDefaultAsync(e => e.Id.ToString() == id));

            if (person == null)
            {
                throw new NotFoundException($"Person with id {id} not found.");
            }



            return person;

        }

        public async Task<PersonResponse> GetSingleByFontysId(string fontysId)
        {
            PersonResponse person = _converter.ModelToDto(await _context.Person.FirstOrDefaultAsync(e => e.FontysId == fontysId));
            return person;
        }
    }
}
