using currency_exchange_calculator.Interfaces;
using currency_exchange_calculator.Services;
using currency_exchange_calculator.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

Console.WriteLine("Current Directory: " + Directory.GetCurrentDirectory());

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
Console.WriteLine($"Environment: {environment}");

var builder = WebApplication.CreateBuilder(args);

// Tulosta kaikki konfiguraation arvot
Console.WriteLine("Configuration values:");
foreach (var kvp in builder.Configuration.AsEnumerable())
{
    Console.WriteLine($"{kvp.Key}: {kvp.Value}");
}

// Testaa konfiguraation lukemista
Console.WriteLine($"MYSQLCONNSTR_localdb: {builder.Configuration.GetConnectionString("MYSQLCONNSTR_localdb")}");

// Configure services
builder.Services.AddHttpClient<ICurrencyService, CurrencyService>();

// Retrieve the connection string for MySQL from configuration and throw an exception if it is not set
var dbConnectionString = builder.Configuration.GetConnectionString("MYSQLCONNSTR_localdb");
if (string.IsNullOrEmpty(dbConnectionString))
{
    throw new InvalidOperationException("The connection string 'MYSQLCONNSTR_localdb' is not configured.");
}

// Register AppDbContext with MySQL database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(dbConnectionString, ServerVersion.AutoDetect(dbConnectionString)));

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
