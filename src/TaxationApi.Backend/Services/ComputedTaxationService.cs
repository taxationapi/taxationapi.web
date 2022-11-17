﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxationApi.Backend.Data;
using TaxationApi.Backend.Helpers.Extensions;
using TaxationApi.Backend.Model.ComputedTaxations;
using TaxationApi.Backend.Model.ComputedTaxations.Requests;
using TaxationApi.Backend.Model.CountryCurrencies;
using TaxationApi.Backend.Model.Taxation;

namespace TaxationApi.Backend.Services
{
    public class ComputedTaxationService :IComputedTaxationService
    {
        private List<TaxationData> _data;
        private ICountryCurrencyService _countryCurrencyService;

        public ComputedTaxationService(ICountryCurrencyService countryCurrencyService)
        {
            _data = Database.LoadTaxationData().Taxations;
            _countryCurrencyService = countryCurrencyService;
        }
        public List<ComputedTaxation> ComputeTaxations(ComputingTaxationRequest request)
        {
            var computedTaxations = new List<ComputedTaxation>();

            var allTaxations = _data.ToList();
            foreach (var taxation in allTaxations)
            {
                var incomeTaxation = taxation.GetIncomeTax(request);
                if (incomeTaxation != null)
                {
                    var compuated = new ComputedTaxation()
                    {
                        IncomeTaxation = incomeTaxation,
                        Alpha2 = taxation.Alpha2,
                        Alpha3 = taxation.Alpha3,
                        Name = taxation.Name
                    };
                    computedTaxations.Add(compuated);
                }
               
            }

            return computedTaxations;
        }
    }
}
