using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxationApi.Backend.Data;
using TaxationApi.Backend.Helpers.Extensions;
using TaxationApi.Backend.Model.ComputedTaxations;
using TaxationApi.Backend.Model.ComputedTaxations.Requests;
using TaxationApi.Backend.Model.Countries;
using TaxationApi.Backend.Model.CountryCurrencies;
using TaxationApi.Backend.Model.Taxation;

namespace TaxationApi.Backend.Services
{
    public class ComputedTaxationService :IComputedTaxationService
    {
        private List<TaxationData> _data;
        private ICountryCurrencyService _countryCurrencyService;
        private ICountryService _countryService;
        public ComputedTaxationService(ICountryCurrencyService countryCurrencyService,
            ICountryService countryService)
        {
            _data = Database.LoadTaxationData().Taxations;
            _countryCurrencyService = countryCurrencyService;
            _countryService = countryService;
        }
        
        public List<ComputedTaxation> ComputeTaxations(ComputingTaxationRequest request)
        {
            var computedTaxations = new List<ComputedTaxation>();

            var allCurrencies = _countryCurrencyService.GetAllCountries();
            var allCountries = _countryService.GetAllCountries();
            var allTaxations = _data.ToList();
            foreach (var taxation in allTaxations)
            {
                var country = allCountries.FirstOrDefault(c => c.Alpha3 == taxation.Alpha3);
                if (country == null)
                {
                    continue;
                }
                
                var rate = allCurrencies.FirstOrDefault(c => c.CurrencyCode == country.CurrencyCode);
                if (rate == null)
                {
                    continue;
                }

                ComputedTaxation taxationToAdd = new ComputedTaxation()
                {
                    Alpha2 = taxation.Alpha2,
                    Alpha3 = taxation.Alpha3,
                    Name = taxation.Name
                };
                
                var incomeTaxation = taxation.GetIncomeTax(request, rate.UsdExchangeRate);
                if (incomeTaxation != null)
                {
                    taxationToAdd.IncomeTaxation = incomeTaxation;
                }


                var wealthTax = taxation.GetWealthTax(request, rate.UsdExchangeRate);
                if (wealthTax != null)
                {
                    taxationToAdd.WealthTaxation = wealthTax;
                }
                

                var corporateTax = taxation.GetCorporateTax(request, rate.UsdExchangeRate);
                if (corporateTax != null)
                {
                    taxationToAdd.CorporateTaxation = corporateTax;
                }


                computedTaxations.Add(taxationToAdd);
            }

            return computedTaxations;
        }
    }
}
