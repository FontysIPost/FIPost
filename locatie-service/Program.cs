const string SECRET_KEY = "this is my custom Secret key for authnetication";
var SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<KestrelServerOptions>(
    builder.Configuration.GetSection("Kestrel"));

// Add context
var connection = builder.Configuration.GetValue<string>("ConnectionString");
builder.Services.AddDbContextPool<LocatieContext>(
    options => options.UseSqlServer(connection));

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_myAllowSpecificOrigins",
          builder =>
          {
              builder.WithOrigins("*")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin();
          });
});

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = "JwtBearer";
        options.DefaultChallengeScheme = "JwtBearer";
    })
    .AddJwtBearer("JwtBearer", jwtOptions =>
    {
        jwtOptions.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = SIGNING_KEY,
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(5)
        };
    });

//Inject services.
builder.Services.AddTransient<ICityService, CityService>();
builder.Services.AddTransient<IBuildingService, BuildingService>();
builder.Services.AddTransient<IRoomService, RoomService>();

//Inject converter.
builder.Services.AddScoped<IDtoConverter<City, CityRequest, CityResponse>, CityDtoConverter>();
builder.Services.AddScoped<IDtoConverter<Building, BuildingRequest, BuildingResponse>, BuildingDtoConverter>();
builder.Services.AddScoped<IDtoConverter<Room, RoomRequest, RoomResponse>, RoomDtoConverter>();

var app = builder.Build();

// Update the database schema if there are any changes
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var dbContext = services.GetRequiredService<LocatieContext>();
dbContext.Database.Migrate();

if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();


app.UseHttpsRedirection();

app.UseCors("_myAllowSpecificOrigins");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "LocatieService");
    // Serve the swagger UI at the app's root
    c.RoutePrefix = string.Empty;
});

app.Run();