using currency_exchange_calculator.Interfaces;
using currency_exchange_calculator.Services;
using currency_exchange_calculator.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddHttpClient<ICurrencyService, CurrencyService>();

// Retrieve the connection string for MySQL from configuration
var dbConnectionString = builder.Configuration.GetConnectionString("MYSQLCONNSTR_localdb");

// Check if the connection string is set, and only register AppDbContext if it is
if (!string.IsNullOrEmpty(dbConnectionString))
{
    // Register AppDbContext with MySQL database
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseMySql(dbConnectionString, ServerVersion.AutoDetect(dbConnectionString)));
}
else
{
    // Optionally log a warning or set a flag to indicate the database is not available
    Console.WriteLine("Warning: The connection string 'MYSQLCONNSTR_localdb' is not configured. Database functionality will be disabled.");
}

// Configure controllers, Swagger documentation, and XML comments
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "currency_exchange_calculator", Version = "v1" });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "currency_exchange_calculator v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapGet("/", () => Results.Json(new { message = "CurrencyConverter API is running" }));

app.MapControllers();

app.Run();
