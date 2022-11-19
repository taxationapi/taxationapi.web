using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxationApi.Backend.Model.Countries
{
    public class Country
    {
        public string Name { get; set; }
        public string Alpha2 { get; set; }
        public string Alpha3 { get; set; }
        public string Region { get; set; }
        public string SubRegion { get; set; }
        public string CurrencyCode { get; set; }
    }
}
