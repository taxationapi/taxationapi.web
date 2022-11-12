using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TaxationApi.Backend.Data
{
    public static class Database
    {
        public static DataModel LoadData()
        {
            var file = System.IO.File.ReadAllText("country_data.json");

            var data = JsonConvert.DeserializeObject<List<CountryData>>(file);
            return new DataModel(){Countries = data };
        }
    }
}
