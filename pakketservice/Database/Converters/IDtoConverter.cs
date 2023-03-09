using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PakketService.Database.Converters
{
    // Useful: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/covariance-contravariance/creating-variant-generic-interfaces
    public interface IDtoConverter<Model, Request, Response>
    {
        Model DtoToModel(Request request);
        Response ModelToDto(Model model);
        List<Response> ModelToDto(List<Model> models);
    }
}
