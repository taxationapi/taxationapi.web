using TaxationApi.Backend.Model.Countries;

namespace TaxationApi.Web.Model.TaxRates
{
    public class GetTaxationDataRequest
    {
        public string? Query { get; set; }
        public Region? Region { get; set; }
        public decimal? MaximumCorporateTax { get; set; }
        public decimal? MaximumCapitalGainsTax { get; set; }
        public decimal? MaximumIncomeTax { get; set; }
        public decimal? MinimumCorporateTax { get; set; }
        public decimal? MinimumCapitalGainsTax { get; set; }
        public decimal? MinimumIncomeTax { get; set; }
        public bool? WealthTaxPossible { get; set; }
        public bool? LumpsumpTaxPossible { get; set; }
        
    }

}
