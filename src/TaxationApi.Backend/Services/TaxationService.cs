﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxationApi.Backend.Data;
using TaxationApi.Backend.Model.Countries;
using TaxationApi.Backend.Model.CountryCurrencies;
using TaxationApi.Backend.Model.Taxation;

namespace TaxationApi.Backend.Services
{
  
    public class TaxationService : ITaxationService
    {
        private List<TaxationData> _data;
        private ICountryCurrencyService _countryCurrencyService;
        private ICountryService _countryService;
      

        public TaxationService(ICountryCurrencyService countryCurrencyService,
            ICountryService countryService)
        {
            _data = Database.LoadTaxationData().Taxations;
            _countryCurrencyService = countryCurrencyService;
            _countryService = countryService;
        }

        public List<TaxationData> GetTaxationData(TaxationSpecification specification)
        {
            var returnSet = _data.ToList();
            FixCurrencies(returnSet);
            FixRegions(returnSet);

            if (!string.IsNullOrWhiteSpace(specification.Query))
            {
                returnSet = returnSet.Where(c => c.Name.Contains(specification.Query)).ToList();
            }
            if(specification.Region != null)
            {
                returnSet = returnSet.Where(c => !string.IsNullOrWhiteSpace(c.Region) && c.Region.Contains(specification.Region.ToString())).ToList();
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
            if (specification.WealthTaxPossible.HasValue)
            {
                if (specification.WealthTaxPossible.Value)
                {
                    returnSet = returnSet.Where(x => x.WealthTax != null).ToList();
                }
                else
                {
                    returnSet = returnSet.Where(x => x.WealthTax == null).ToList();
                }
            }


           return returnSet;
        }

        private void FixRegions(List<TaxationData> taxationDatas)
        {
            var countries = _countryService.GetAllCountries();
            foreach (var country in taxationDatas)
            {
              var countryData = countries.FirstOrDefault(c => c.Alpha3 == country.Alpha3);
                    if (countryData != null)
                    {
                        country.Region = countryData.Region;
                    }
            }
        }
        private void FixCurrencies(List<TaxationData> taxationDatas)
        {
            var countries = Database.LoadCountryData();
            foreach (var country in taxationDatas)
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

                if (country.WealthTax != null)
                {
                    country.WealthTax.Currency = currency;
                }

                if (country.IncomeTax != null)
                {
                    foreach (var bracket in country.IncomeTax.Brackets)
                    {
                        bracket.Currency = currency;
                    }
                }
                if (country.CorporateTax != null)
                {
                    foreach (var bracket in country.CorporateTax.Brackets)
                    {
                        bracket.Currency = currency;
                    }
                }

            }
        }
    }
}
