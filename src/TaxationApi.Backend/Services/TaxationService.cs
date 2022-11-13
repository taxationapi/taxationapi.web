using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxationApi.Backend.Data;
using TaxationApi.Backend.Model.CountryCurrencies;
using TaxationApi.Backend.Model.Taxation;

namespace TaxationApi.Backend.Services
{
  
    public class TaxationService : ITaxationService
    {
        private List<TaxationData> _data;
        private ICountryCurrencyService _countryCurrencyService;

        public TaxationService(ICountryCurrencyService countryCurrencyService)
        {
            _data = Database.LoadTaxationData().Taxations;
            _countryCurrencyService = countryCurrencyService;
        }

        public List<TaxationData> GetTaxationData(TaxationSpecification specification)
        {
            var returnSet = _data.ToList();

            if(!string.IsNullOrWhiteSpace(specification.Query))
            {
                returnSet = returnSet.Where(c => c.Name.Contains(specification.Query)).ToList();
            }
            if (specification.MaximumCorporateTax.HasValue)
            {
                returnSet = returnSet.Where(x => x.CorporateTax != null &&  x.CorporateTax.Rate <= specification.MaximumCorporateTax.Value).ToList();
            }
            if (specification.MaximumCapitalGainsTax.HasValue)
            {
                returnSet = returnSet.Where(x => x.CapitalGainsTax != null && x.CapitalGainsTax.Rate <= specification.MaximumCapitalGainsTax.Value).ToList();
            }
            if (specification.MaximumIncomeTax.HasValue)
            {
                returnSet = returnSet.Where(x => x.IncomeTax != null && x.IncomeTax.Rate <= specification.MaximumIncomeTax.Value).ToList();
            }
            if (specification.MinimumCorporateTax.HasValue)
            {
                returnSet = returnSet.Where(x => x.CorporateTax != null && x.CorporateTax.Rate >= specification.MinimumCorporateTax.Value).ToList();
            }
            if (specification.MinimumCapitalGainsTax.HasValue)
            {
                returnSet = returnSet.Where(x => x.CorporateTax != null && x.CorporateTax.Rate >= specification.MinimumCapitalGainsTax.Value).ToList();
            }
            if (specification.MinimumIncomeTax.HasValue)
            {
                returnSet = returnSet.Where(x => x.IncomeTax != null && x.CorporateTax.Rate >= specification.MinimumIncomeTax.Value).ToList();
            }

            if (specification.LumpsumpTaxPossible.HasValue)
            {
                if (specification.LumpsumpTaxPossible.Value)
                {
                    returnSet = returnSet.Where(x => x.LumpsumpTax != null).ToList();
                }
                else
                {
                    returnSet = returnSet.Where(x => x.LumpsumpTax == null).ToList();
                }
            }
            
            FixCurrencies(returnSet);
            return returnSet;
        }

        private void FixCurrencies(List<TaxationData> taxationDatas)
        {
            var countries = _countryCurrencyService.GetAllCountries();
            foreach(var country in taxationDatas)
            {
                string currency = string.Empty;
                var currency1 = countries.FirstOrDefault(c => c.Alpha3 == country.Alpha3);

                if (currency1 != null)
                {
                    currency = currency1.CurrencyCode;
                }

                if (country.LumpsumpTax != null)
                {
                    country.LumpsumpTax.Currency = currency;
                }
            }
        }
    }
}
