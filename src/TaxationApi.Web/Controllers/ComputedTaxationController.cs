using Microsoft.AspNetCore.Mvc;
using TaxationApi.Backend.Model.ComputedTaxations;
using TaxationApi.Backend.Model.ComputedTaxations.Requests;
using TaxationApi.Backend.Model.Taxation;
using TaxationApi.Web.Model.TaxRates;

namespace TaxationApi.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComputedTaxationController : ControllerBase
    {
        private IComputedTaxationService _computedTaxationService;

        public ComputedTaxationController(IComputedTaxationService computedTaxationService)
        {
            _computedTaxationService = computedTaxationService;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] ComputingTaxationRequest getRequest)
        {

            var result = _computedTaxationService.ComputeTaxations(getRequest);
          //  result = result.Where(c => c.Alpha3 == "AUT").ToList();
            return Ok(result);
        }
    }
}
