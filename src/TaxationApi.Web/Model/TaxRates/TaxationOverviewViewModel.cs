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
    }

    public class TaxationOverViewEntityCorporateViewModel
    {
        public decimal Rate { get; set; }
        public DateTime LastUpdated { get; set; }
    }


    public class TaxationOverViewEntityCapitalGainsViewModel
    {
        public decimal Rate { get; set; }
        public DateTime LastUpdated { get; set; }
    }

    public class TaxationOverViewEntityIncomeViewModel
    {
        public decimal Rate { get; set; }
        public DateTime LastUpdated { get; set; }
    }

}
