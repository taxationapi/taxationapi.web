using Microsoft.AspNetCore.Mvc;
using TaxationApi.Backend.Model.Taxation;
using TaxationApi.Web.Model.TaxRates;

namespace TaxationApi.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaxRatesController : ControllerBase
    {
        private ITaxationService _taxationService;
        public TaxRatesController(ITaxationService taxationService)
        {
            _taxationService = taxationService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var data = _taxationService.GetTaxationData();

            var taxations = new TaxationOverviewViewModel();
            foreach (var datapoint in data)
            {
                var entity = new TaxationOverViewEntityViewModel()
                {
                    Alpha2 = datapoint.Alpha2,
                    Alpha3 = datapoint.Alpha3,
                    Name = datapoint.Name
                };

                entity.CorporateTax = new TaxationOverViewEntityCorporateViewModel()
                {
                    LastUpdated = datapoint.corporatetaxlastupdate,
                    Rate = datapoint.corporatetax
                };

                taxations.Taxations.Add(entity);

            }

            return Ok(taxations);
        }
    }
}
