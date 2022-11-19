using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TaxationApi.Backend.Model.Taxation;

namespace TaxationApi.Backend.Data
{
    public static class Database
    {
        public static CountryCurrenciesDatabaseModel LoadCountryCurrencies()
        {
            var file = System.IO.File.ReadAllText("country_currencies.json");

            var data = JsonConvert.DeserializeObject<CountryCurrenciesDatabaseModel>(file);
            return data;
        }

        public static TaxationDatabaseModel LoadTaxationData()
        {
            var file = System.IO.File.ReadAllText("country_data.json");

            var data = JsonConvert.DeserializeObject<TaxationDatabaseModel>(file);
            return data;
        }

        public static CountryMetadataDatabaseModel LoadCountryData()
        {
            var file = System.IO.File.ReadAllText("country_metadata.json");

            var data = JsonConvert.DeserializeObject<CountryMetadataDatabaseModel>(file);
            return data;
        }



    }


}
