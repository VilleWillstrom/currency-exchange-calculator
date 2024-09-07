using CurrencyConverterAPI.Interfaces;
using CurrencyConverterAPI.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Lisää palvelut kontrollerille ja Swaggerille
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CurrencyConverterAPI", Version = "v1" });
});

// Rekisteröi ICurrencyService ja CurrencyService
builder.Services.AddHttpClient<ICurrencyService, CurrencyService>();

var app = builder.Build();

// Määritä reititys ja Swagger
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
app.MapControllers();
app.Run();
