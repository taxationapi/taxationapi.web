using Newtonsoft.Json.Linq;
using TaxationApi.Backend.Model.ComputedTaxations;
using TaxationApi.Backend.Model.ComputedTaxations.Requests;
using TaxationApi.Backend.Services;

namespace Taxation.Backend.Tests.ComputationServices
{
    public class ComputationNetworthServicesTests
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
        public void NetworthTaxXero()
        {
            var incomeRes = _computedTaxationService.ComputeTaxations(new ComputingTaxationRequest()
            {
                TotalWealth = 50000000000
            });

            Assert.AreEqual(incomeRes.Single(c => c.Alpha2 == "DK").YearlyTotalTax, 0.0m);
            Assert.AreEqual(incomeRes.Single(c => c.Alpha2 == "EG").YearlyTotalTax, 0.0m);
            Assert.AreEqual(incomeRes.Single(c => c.Alpha2 == "KW").YearlyTotalTax, 0.0m);
            Assert.AreEqual(incomeRes.Single(c => c.Alpha2 == "LB").YearlyTotalTax, 0.0m);


            Assert.Pass();
        }

        [Test]
        public void ArgentinaNetworthTax()
        {
            var incomeRes = _computedTaxationService.ComputeTaxations(new ComputingTaxationRequest()
            {
                TotalWealth = 50000000
            });

            var argentina = incomeRes.Single(c => c.Alpha2 == "AR");
            var isOk = Math.Abs(argentina.YearlyTotalTax - 842816m) <= 5m;

            Assert.IsTrue(isOk);

            Assert.Pass();
        }

        [Test]
        public void ArgentinaNetworthTaxFromLow()
        {
            var incomeRes = _computedTaxationService.ComputeTaxations(new ComputingTaxationRequest()
            {
                TotalWealth = 50000
            });

            Assert.AreEqual(incomeRes.Single(c => c.Alpha2 == "AR").YearlyTotalTax, 0.0m);

            Assert.Pass();
        }

        [Test]
        public void FranceNetworthTax()
        {
            var incomeRes = _computedTaxationService.ComputeTaxations(new ComputingTaxationRequest()
            {
                TotalWealth = 15000000
            });

            var argentina = incomeRes.Single(c => c.Alpha2 == "FR");
            var isOk = Math.Abs(argentina.YearlyTotalTax - 69788m) <= 5m;

            Assert.IsTrue(isOk);

            Assert.Pass();
        }

        [Test]
        public void FranceNetworthTaxFromLow()
        {
            var incomeRes = _computedTaxationService.ComputeTaxations(new ComputingTaxationRequest()
            {
                TotalWealth = 50000
            });

            Assert.AreEqual(incomeRes.Single(c => c.Alpha2 == "FR").YearlyTotalTax, 0.0m);

            Assert.Pass();
        }


    }
}