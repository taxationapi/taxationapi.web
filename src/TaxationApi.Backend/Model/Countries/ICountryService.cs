using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxationApi.Backend.Model.Countries
{
    public interface ICountryService
    {
        List<Country> GetAllCountries();
    }
}
