using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxationApi.Backend.Model.CountryCurrencies
{
    public class CountryCurrency
    {
        public string Alpha2 { get; set; }
        public string Alpha3 { get; set; }
        public string CurrencyCode { get; set; }
    }
}
