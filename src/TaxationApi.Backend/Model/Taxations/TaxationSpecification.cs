using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxationApi.Backend.Model.Taxation
{
    public class TaxationSpecification
    {
        public string? Query { get; set; }
        public decimal? MaximumCorporateTax { get; set; }
        public decimal? MaximumCapitalGainsTax { get; set; }
        public decimal? MaximumIncomeTax { get; set; }
        public decimal? MinimumCorporateTax { get; set; }
        public decimal? MinimumCapitalGainsTax { get; set; }
        public decimal? MinimumIncomeTax { get; set; }
        public bool? LumpsumpTaxPossible { get; set; }
        public bool? WealthTaxPossible { get; set; }
    }
}
