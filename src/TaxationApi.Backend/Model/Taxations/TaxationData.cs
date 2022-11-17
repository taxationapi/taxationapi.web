using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxationApi.Backend.Model.Taxations;

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
        public LumpsumpTaxationData LumpsumpTax { get; set; }
        public WealthTaxTaxationData WealthTax { get; set; }

    }


    public class CorporateTaxationData
    {
        public decimal Rate { get; set; }
        public DateTime LastUpdated { get; set; }
        public ValidationLevel ValidationLevel { get; set; }
        public List<TaxationBracket> Brackets { get; set; }

        public CorporateTaxationData()
        {
            Brackets = new List<TaxationBracket>();
        }
    }


    public class CapitalGainsTaxationData
    {
        public decimal Rate { get; set; }
        public DateTime LastUpdated { get; set; }
        public ValidationLevel ValidationLevel { get; set; }
        public List<TaxationBracket> Brackets { get; set; }

        public CapitalGainsTaxationData()
        {
            Brackets = new List<TaxationBracket>();
        }
    }

    public class WealthTaxTaxationData
    {
        public decimal Rate { get; set; }
        public decimal Base { get; set; }
        public string Comments { get; set; }
        public ValidationLevel ValidationLevel { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Currency { get; set; }
        public List<TaxationBracket> Brackets { get; set; }

        public WealthTaxTaxationData()
        {
            Brackets = new List<TaxationBracket>();
        }
    }

    public class IncomeTaxationData
    {
        public decimal Rate { get; set; }
        public DateTime LastUpdated { get; set; }
        public ValidationLevel ValidationLevel { get; set; }
        public List<TaxationBracket> Brackets { get; set; }

        public IncomeTaxationData()
        {
            Brackets = new List<TaxationBracket>();
        }
    }

    public class LumpsumpTaxationData
    {
        public decimal Amount { get; set; }
        public decimal? Rate { get; set; }
        public string Currency { get; set; }
        public ValidationLevel ValidationLevel { get; set; }
        public DateTime LastUpdated { get; set; }
    }

    public class TaxationBracket
    {
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        public string Currency { get; set; }
        public ValidationLevel ValidationLevel { get; set; }
        public decimal LowerBracket { get; set; }
        public decimal HigherBracket { get; set; }

        public decimal BracketSize
        {
            get
            {
                return HigherBracket - LowerBracket;
            }
        }

    }
}
