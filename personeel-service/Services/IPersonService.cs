using personeel_service.Models;
using personeel_service.Models.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace personeel_service.Services
{
    public interface IPersonService
    {
        Task<List<PersonResponse>> GetAllAsync();
        // List<Person> GetAll();
        // Person GetById(string id);
        Task<PersonResponse> GetByIdAsync(string id);
        Task<PersonResponse> GetSingleByFontysId(string fontysId);
    }
}
