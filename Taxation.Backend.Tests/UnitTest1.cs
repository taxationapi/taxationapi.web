using TaxationApi.Backend.Model.ComputedTaxations;
using TaxationApi.Backend.Model.ComputedTaxations.Requests;
using TaxationApi.Backend.Services;

namespace Taxation.Backend.Tests
{
    public class Tests
    {
        private IComputedTaxationService _computedTaxationService;

        [SetUp]
        public void Setup()
        {
            var countryCurrency = new CountryCurrencyService();
            var countryService = new CountryService();
            _computedTaxationService = new ComputedTaxationService(countryCurrency,countryService);
        }

        [Test]
        public void Test1()
        {
            var incomeRes = _computedTaxationService.ComputeTaxations(new ComputingTaxationRequest()
            {
                YearlyIncome = 100000
            });


            Assert.Pass();
        }
    }
}