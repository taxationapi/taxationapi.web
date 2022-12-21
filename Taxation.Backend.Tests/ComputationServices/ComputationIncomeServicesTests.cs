using TaxationApi.Backend.Model.ComputedTaxations;
using TaxationApi.Backend.Model.ComputedTaxations.Requests;
using TaxationApi.Backend.Services;

namespace Taxation.Backend.Tests.ComputationServices
{
    public class ComputationIncomeServicesTests
    {
        private IComputedTaxationService _computedTaxationService;

        [SetUp]
        public void Setup()
        {
            var countryCurrency = new CountryCurrencyService();
            var countryService = new CountryService();
            _computedTaxationService = new ComputedTaxationService(countryCurrency, countryService);
        }

        [Test]
        public void IncomeTaxHundredKNoBracketsTests()
        {
            var incomeRes = _computedTaxationService.ComputeTaxations(new ComputingTaxationRequest()
            {
                YearlyIncome = 100000
            });

            Assert.AreEqual(incomeRes.Single(c => c.Alpha2 == "AF").YearlyTotalTax, 20000m);
            Assert.AreEqual(incomeRes.Single(c => c.Alpha2 == "FJ").YearlyTotalTax, 20000m);
            Assert.AreEqual(incomeRes.Single(c => c.Alpha2 == "ML").YearlyTotalTax, 30000m);
            Assert.AreEqual(incomeRes.Single(c => c.Alpha2 == "NG").YearlyTotalTax, 24000m);

            Assert.Pass();
        }

        [Test]
        public void IncomeTaxFiveHundredKNoBracketsTests()
        {
            var incomeRes = _computedTaxationService.ComputeTaxations(new ComputingTaxationRequest()
            {
                YearlyIncome = 50000
            });

            Assert.AreEqual(incomeRes.Single(c => c.Alpha2 == "OM").YearlyTotalTax, 0m);
            Assert.AreEqual(incomeRes.Single(c => c.Alpha2 == "NE").YearlyTotalTax, 12000m);
            Assert.AreEqual(incomeRes.Single(c => c.Alpha2 == "MM").YearlyTotalTax, 12500m);
            Assert.AreEqual(incomeRes.Single(c => c.Alpha2 == "MN").YearlyTotalTax, 10000m);

            Assert.Pass();
        }


    }
}