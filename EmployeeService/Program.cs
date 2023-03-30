using System;
using System.Text;
using EmployeeService.Database.Contexts;
using EmployeeService.Database.Converters;
using EmployeeService.Models;
using EmployeeService.Models.DTO_s;
using EmployeeService.Services;
using Microsoft.IdentityModel.Tokens;

const string SECRET_KEY = "this is my custom Secret key for authnetication";
SymmetricSecurityKey SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connection = builder.Configuration.GetValue<string>("ConnectionString");
builder.Services.AddDbContextPool<PersonServiceContext>(
    options => options.UseSqlServer(connection));

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("_myAllowSpecificOrigins",
        builder =>
        {
            builder
                .WithOrigins("*")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
        });
});

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = "JwtBearer";
        options.DefaultChallengeScheme = "JwtBearer";
    }).AddJwtBearer("JwtBearer", jwtOptions =>
    {
        jwtOptions.TokenValidationParameters = new TokenValidationParameters()
        {
            IssuerSigningKey = SIGNING_KEY,
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(5)
        };
    });

// Inject services.
builder.Services.AddTransient<IPersonService, PersonService>();

//Inject converter.
builder.Services.AddScoped<IDtoConverter<Person, PersonRequest, PersonResponse>, DtoConverter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

app.UseCors("_myAllowSpecificOrigins");

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger(c => { c.SerializeAsV2 = true; });

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
// specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "personeel-service");
    // Serve the swagger UI at the app's root
    c.RoutePrefix = string.Empty;
});

app.Run();