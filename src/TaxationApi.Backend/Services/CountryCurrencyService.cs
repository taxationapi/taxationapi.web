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
    public class CountryCurrencyService : ICountryCurrencyService
    {
        private List<CountryCurrency> _data;

        public CountryCurrencyService()
        {
            _data = Database.LoadCountryCurrencies();
        }


        public List<CountryCurrency> GetAllCountries()
        {
            var returnSet = _data.ToList();
            return returnSet;
        }
    }
}
