using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxationApi.Backend.Data
{
    public class DataModel
    {
        public List<CountryData> Countries { get; set; }
    }

    public class CountryData
    {
        public string Name { get; set; }
        public string Alpha2 { get; set; }
        public string Alpha3 { get; set; }
        public decimal corporatetax { get; set; }
        public DateTime corporatetaxlastupdate { get; set; }
    }

}
