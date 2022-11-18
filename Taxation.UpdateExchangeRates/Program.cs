using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using TaxationApi.Backend.Data;
using TaxationApi.Backend.Model.CountryCurrencies;
using TaxationApi.Backend.Model.ExchangeRates;

var allCurrencies = Database.LoadCountryCurrencies();

List<CountryCurrency> dataWestore = new List<CountryCurrency>();
foreach (var currency in allCurrencies)
{
    if (dataWestore.Any(c => currency.CurrencyCode == c.CurrencyCode))
        continue;

    Console.WriteLine("Pulling data for " + currency.CurrencyCode);

    try
    {
        var getExchangeRate = GetByBase("USD", currency.CurrencyCode);
        Console.WriteLine("USD rate is: " + getExchangeRate);

        dataWestore.Add(new CountryCurrency()
        {
            Alpha2 = currency.Alpha2,
            CurrencyCode = currency.CurrencyCode,
            Alpha3 = currency.Alpha3,
            UsdExchangeRate = getExchangeRate.Rate
        });
    }
    catch (Exception e)
    {
        Console.WriteLine("Failed to get currency data for " + currency.CurrencyCode);
    }
  
    
}

Console.WriteLine("Starting saving file");

var savePath = "country_currencies_updated.json";
var dataToStore = JsonConvert.SerializeObject(dataWestore);
File.WriteAllText(savePath, dataToStore);


Console.WriteLine("Finished writing");
Console.ReadKey();

ExchangeRate GetByBase(string currencyBase, string symbols)
{
    string config = "";

    if (string.IsNullOrWhiteSpace(config))
        throw new ArgumentException("Yeag please add an API key :D ");

    var url = string.Format("https://api.apilayer.com/exchangerates_data/convert?to={1}&from={0}&amount=1",
        currencyBase, symbols);
    var apikey = config;
    using (var client = new HttpClient())
    {
        client.DefaultRequestHeaders.Add("apikey", apikey);
        var task = Task.Run(() => client.GetStringAsync(url));
        task.Wait();

        var response = task.Result;

        dynamic dataFromApi = JsonConvert.DeserializeObject(response);

        ExchangeRate data = new ExchangeRate()
        {
            Base = currencyBase,
            Success = dataFromApi.success,
            Symbol = symbols,
            Rate = dataFromApi.result
        };
        return data;
    }
}