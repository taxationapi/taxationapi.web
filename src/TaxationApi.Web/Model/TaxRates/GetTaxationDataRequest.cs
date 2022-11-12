namespace TaxationApi.Web.Model.TaxRates
{
    public class GetTaxationDataRequest
    {
        public string? Query { get; set; }
        public decimal? MaximumCorporateTax { get; set; }
        public decimal? MaximumCapitalGainsTax { get; set; }

    }
}
