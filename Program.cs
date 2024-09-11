using CurrencyConverterAPI.Interfaces;
using CurrencyConverterAPI.Services;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CurrencyConverterAPI", Version = "v1" });
});

// Register ICurrencyService and CurrencyService
builder.Services.AddHttpClient<ICurrencyService, CurrencyService>();

var app = builder.Build();

// Define routing and Swagger. Swagger is available only at development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CurrencyConverterAPI v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Define default route to return an empty JSON object
app.MapGet("/", () => Results.Json(new { }));

// Define routing for controllers
app.MapControllers();

app.Run();
