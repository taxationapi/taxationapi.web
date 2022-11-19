using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxationApi.Backend.Model.Taxation;

namespace TaxationApi.Backend.Model.CountryCurrencies
{
    public interface ICountryCurrencyService
    {
        CountryCurrency GetByCurrencyCode(string code);
        List<CountryCurrency> GetAllCountries();
    }
}
