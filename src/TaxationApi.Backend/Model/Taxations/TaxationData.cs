using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxationApi.Backend.Model.Taxation
{
    public class TaxationData
    {
        public string Name { get; set; }
        public string Alpha2 { get; set; }
        public string Alpha3 { get; set; }
        public CorporateTaxationData CorporateTax { get; set; }
        public CapitalGainsTaxationData CapitalGainsTax { get; set; }
        public IncomeTaxationData IncomeTax { get; set; }
    }


    public class CorporateTaxationData
    {
        public decimal Rate { get; set; }
        public DateTime LastUpdated { get; set; }
    }


    public class CapitalGainsTaxationData
    {
        public decimal Rate { get; set; }
        public DateTime LastUpdated { get; set; }
    }

    public class IncomeTaxationData
    {
        public decimal Rate { get; set; }
        public DateTime LastUpdated { get; set; }
    }

}
