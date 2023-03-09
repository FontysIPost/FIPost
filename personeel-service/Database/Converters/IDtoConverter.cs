using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace personeel_service.Database.Converters
{
    public interface IDtoConverter<Model, Request, Response>
    {
        Model DtoToModel(Request request);
        Response ModelToDto(Model model);
        List<Response> ModelToDto(List<Model> models);
    }
}
