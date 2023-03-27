using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using personeel_service.Database.Contexts;
using personeel_service.Database.Converters;
using personeel_service.Database.DTO_s;
using personeel_service.Models;
using personeel_service.Models.DTO_s;
using personeel_service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connection = builder.Configuration.GetValue<string>("ConnectionString");
builder.Services.AddDbContextPool<PersonServiceContext>(
    options => options.UseSqlServer(connection));

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_myAllowSpecificOrigins",
        builder =>
        {
            builder
                .WithOrigins("*")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
        });
});

// Inject services.
builder.Services.AddTransient<IPersonService, PersonService>();

//Inject converter.
builder.Services.AddScoped<IDtoConverter<Person, PersonRequest, PersonResponse>, DtoConverter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors("_myAllowSpecificOrigins");

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger(c =>
{
    c.SerializeAsV2 = true;
});

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
// specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "personeel-service");
    // Serve the swagger UI at the app's root
    c.RoutePrefix = string.Empty;
});

app.Run();