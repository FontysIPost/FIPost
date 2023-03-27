using System.Collections.Generic;

namespace EmployeeService.Database.Converters;

public interface IDtoConverter<Model, Request, Response>
{
    Model DtoToModel(Request request);
    Response ModelToDto(Model model);
    List<Response> ModelToDto(List<Model> models);
}