const string SECRET_KEY = "this is my custom Secret key for authnetication";
var SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));

var builder = WebApplication.CreateBuilder(args);

// Add context
var connection = builder.Configuration.GetValue<string>("ConnectionString");
builder.Services.AddDbContextPool<PackageServiceContext>(
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
    
// Inject services.
builder.Services.AddTransient<IPackageService, PackageService>();
builder.Services.AddTransient<ITicketService, TicketService>();

//Inject converter.
builder.Services.AddScoped<IDtoConverter<Package, PackageRequest, PackageResponse>, DtoConverter>();
builder.Services.AddScoped<IDtoConverter<Ticket, TicketRequest, TicketResponse>, TicketDtoConverter>();

var app = builder.Build();

// Update the database schema if there are any changes
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var dbContext = services.GetRequiredService<PackageServiceContext>();
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
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PakketService");
    // Serve the swagger UI at the app's root
    c.RoutePrefix = string.Empty;
});

app.Run();