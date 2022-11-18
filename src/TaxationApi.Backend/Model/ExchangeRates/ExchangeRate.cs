using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxationApi.Backend.Model.ExchangeRates
{
    public class ExchangeRate
    {
        public string Base { get; set; }
        public string Symbol { get; set; }
        public decimal Rate { get; set; }
        public bool Success { get; set; }
    }
}
