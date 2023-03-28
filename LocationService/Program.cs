var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

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
builder.Services.AddDbContextPool<LocatieContext>(
    options => options.UseSqlServer(connection));

// Inject services.
builder.Services.AddTransient<ICityService, CityService>();
builder.Services.AddTransient<IBuildingService, BuildingService>();
builder.Services.AddTransient<IRoomService, RoomService>();

// Inject converters.
builder.Services.AddScoped<IDtoConverter<City, CityRequest, CityResponse>, CityDtoConverter>();
builder.Services.AddScoped<IDtoConverter<Building, BuildingRequest, BuildingResponse>, BuildingDtoConverter>();
builder.Services.AddScoped<IDtoConverter<Room, RoomRequest, RoomResponse>, RoomDtoConverter>();

var app = builder.Build();

// Migrate database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var addressContext = services.GetRequiredService<LocatieContext>();
    addressContext.Database.Migrate();
}

if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();


app.UseCors("_myAllowSpecificOrigins");

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger(c => { c.SerializeAsV2 = true; });

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
// specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "LocatieService");
    // Serve the swagger UI at the app's root
    c.RoutePrefix = string.Empty;
});

app.Run();