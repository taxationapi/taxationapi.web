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
        public static List<TaxationData> LoadData()
        {
            var file = System.IO.File.ReadAllText("country_data.json");

            var data = JsonConvert.DeserializeObject<TaxationDatalist>(file);
            return data.Taxations;
        }
    }

    public class TaxationDatalist
    {
        public List<TaxationData> Taxations { get; set; }

        public TaxationDatalist()
        {
            Taxations = new List<TaxationData>();
        }
    }
}
