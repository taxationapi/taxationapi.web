using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxationApi.Backend.Data;
using TaxationApi.Backend.Model.Countries;
using TaxationApi.Backend.Model.CountryCurrencies;

namespace TaxationApi.Backend.Services
{
    public class CountryService : ICountryService
    {
        private List<Country> _data;
        public CountryService()
        {
            _data = Database.LoadCountryData();
        }

        public List<Country> GetAllCountries()
        {
            return _data;
        }
    }
}
