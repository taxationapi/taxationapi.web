using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxationApi.Backend.Model.ComputedTaxations.Requests
{
    public class ComputingTaxationRequest
    {
        public decimal YearlyIncome { get; set; }
        public decimal YearlyCorporateProfits { get; set; }
        public decimal YearlyCapitalGains { get; set; }
        public decimal? TotalWealth { get; set; }
        public string Currency { get; set; }
    }
}
