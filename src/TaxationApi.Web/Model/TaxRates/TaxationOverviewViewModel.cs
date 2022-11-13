namespace TaxationApi.Web.Model.TaxRates
{
    public class TaxationOverviewViewModel
    {
        public List<TaxationOverViewEntityViewModel> Taxations { get; set; }

        public TaxationOverviewViewModel()
        {
            Taxations = new List<TaxationOverViewEntityViewModel>();
        }
    }

    public class TaxationOverViewEntityViewModel
    {
        public string Name { get; set; }
        public string Alpha2 { get; set; }
        public string Alpha3 { get; set; }
        public TaxationOverViewEntityCorporateViewModel CorporateTax { get; set; }
        public TaxationOverViewEntityCapitalGainsViewModel CapitalGainsTax { get; set; }
        public TaxationOverViewEntityIncomeViewModel IncomeTax { get; set; }
        public TaxationOverViewEntityLumpSumpViewModel LumpsumpTax { get; set; }
    }

    public class TaxationOverViewEntityCorporateViewModel
    {
        public decimal Rate { get; set; }
        public DateTime LastUpdated { get; set; }
        public List<TaxationBracketEntityViewModel> Brackets { get; set; }

        public TaxationOverViewEntityCorporateViewModel()
        {
            Brackets = new List<TaxationBracketEntityViewModel>();
        }
    }

    public class TaxationOverViewEntityLumpSumpViewModel
    {
        public decimal Amount { get; set; }
        public decimal? Rate { get; set; }
        public DateTime LastUpdated { get; set; }

    }


    public class TaxationOverViewEntityCapitalGainsViewModel
    {
        public decimal Rate { get; set; }
        public DateTime LastUpdated { get; set; }
        public List<TaxationBracketEntityViewModel> Brackets { get; set; }

        public TaxationOverViewEntityCapitalGainsViewModel()
        {
            Brackets = new List<TaxationBracketEntityViewModel>();
        }
    }

    public class TaxationOverViewEntityIncomeViewModel
    {
        public decimal Rate { get; set; }
        public DateTime LastUpdated { get; set; }
        public List<TaxationBracketEntityViewModel> Brackets { get; set; }

        public TaxationOverViewEntityIncomeViewModel()
        {

            Brackets = new List<TaxationBracketEntityViewModel>();
        }
    }

    public class TaxationBracketEntityViewModel
    {
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        public string Currency { get; set; }
        public decimal LowerBracket { get; set; }
        public decimal HigherBracket { get; set; }

    }

}
