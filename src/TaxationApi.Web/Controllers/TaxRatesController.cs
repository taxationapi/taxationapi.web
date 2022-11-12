using Mapster;
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
        public IActionResult GetAll([FromQuery] GetTaxationDataRequest getRequest)
        {
            var taxationSpec = getRequest.Adapt<TaxationSpecification>();
            var data = _taxationService.GetTaxationData(taxationSpec);

            var taxations = new TaxationOverviewViewModel();
            foreach (var datapoint in data)
            {
                var adaptedData = datapoint.Adapt<TaxationOverViewEntityViewModel>();
                taxations.Taxations.Add(adaptedData);
            }

            return Ok(taxations);
        }

        [HttpGet("{alpha2}")]
        public IActionResult GetByAlpha2(string alpha2)
        {
            var data = _taxationService.GetTaxationData(new TaxationSpecification()).Where(x => x.Alpha2 == alpha2).ToList();

            var taxations = new TaxationOverviewViewModel();
            foreach (var datapoint in data)
            {
                var adaptedData = datapoint.Adapt<TaxationOverViewEntityViewModel>();
                taxations.Taxations.Add(adaptedData);
            }

            return Ok(taxations.Taxations.FirstOrDefault());
        }


    }
}
