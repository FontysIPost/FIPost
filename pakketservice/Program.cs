using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PakketService.Database.Contexts;
using PakketService.Database.Converters;
using PakketService.Database.Datamodels;
using PakketService.Database.Datamodels.Dtos;
using PakketService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<KestrelServerOptions>(
    builder.Configuration.GetSection("Kestrel"));

builder.Services.AddControllers();
// Add services to the container.
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

// Add context
var connection = builder.Configuration.GetValue<string>("ConnectionString");
builder.Services.AddDbContextPool<PackageServiceContext>(
    options => options.UseSqlServer(connection));

// Inject services.
builder.Services.AddTransient<IPackageService, PackageService>();
builder.Services.AddTransient<ITicketService, TicketService>();

// Inject converters.
builder.Services.AddScoped<IDtoConverter<Package, PackageRequest, PackageResponse>, DtoConverter>();
builder.Services.AddScoped<IDtoConverter<Ticket, TicketRequest, TicketResponse>, TicketDtoConverter>();

var app = builder.Build();

// Migrate database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var addressContext = services.GetRequiredService<PackageServiceContext>();
    addressContext.Database.Migrate();
}

if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();


app.UseCors("_myAllowSpecificOrigins");

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.UseSwagger(c =>
{
    c.SerializeAsV2 = true;
});

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PakketService");
    c.RoutePrefix = string.Empty;
});


app.Run();