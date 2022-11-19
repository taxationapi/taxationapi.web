using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxationApi.Backend.Model.CountryCurrencies;
using TaxationApi.Backend.Model.ExchangeRates;

namespace TaxationApi.Backend.Services
{
    public class ExchangeRateService : IExchangeRateService
    {

        private ICountryCurrencyService _countryCurrencyService;
        public ExchangeRateService(ICountryCurrencyService countryCurrencyServie)
        {
            _countryCurrencyService = countryCurrencyServie;
        }

        public decimal Convert(decimal amount, string mainCurrency, string toCurrency)
        {
            decimal convertTowards = 1.0m;
            if (mainCurrency != "USD")
            {
                var exchangeRate = 1/_countryCurrencyService.GetByCurrencyCode(mainCurrency).UsdExchangeRate;
                convertTowards = exchangeRate;
            }
            decimal amountToConvert = amount * convertTowards;

            var exchangeRateTowardsMain = _countryCurrencyService.GetByCurrencyCode(toCurrency);

            return amountToConvert * exchangeRateTowardsMain.UsdExchangeRate;
        }
    }
}
