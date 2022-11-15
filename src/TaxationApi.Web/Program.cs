using TaxationApi.Backend.Model.ComputedTaxations;
using TaxationApi.Backend.Model.CountryCurrencies;
using TaxationApi.Backend.Model.Taxation;
using TaxationApi.Backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<ITaxationService, TaxationService>();
builder.Services.AddScoped<IComputedTaxationService, ComputedTaxationService>();
builder.Services.AddScoped<ICountryCurrencyService, CountryCurrencyService>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.RoutePrefix = "";
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
