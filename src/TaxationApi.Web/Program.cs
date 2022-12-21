using Microsoft.OpenApi.Models;
using TaxationApi.Backend.Model.ComputedTaxations;
using TaxationApi.Backend.Model.Countries;
using TaxationApi.Backend.Model.CountryCurrencies;
using TaxationApi.Backend.Model.ExchangeRates;
using TaxationApi.Backend.Model.Taxation;
using TaxationApi.Backend.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<ITaxationService, TaxationService>();
builder.Services.AddScoped<IComputedTaxationService, ComputedTaxationService>();
builder.Services.AddScoped<ICountryCurrencyService, CountryCurrencyService>();
builder.Services.AddScoped<IExchangeRateService, ExchangeRateService>();
builder.Services.AddScoped<ICountryService, CountryService>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Taxation API - all taxation in the world", 
        Version = "v1",
        Description = "With this API you can get all the tax rates in the world, and also compute how much someones tax would look",
        Contact = new OpenApiContact()
        {
            Email = "mcoroklo@gmail.com",
            Name = "Lars Holdgaard"
        }
    });
});

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});
builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
