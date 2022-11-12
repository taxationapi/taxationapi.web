using Microsoft.AspNetCore.Mvc;
using TaxationApi.Backend.Model.Taxation;

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
            return Ok("ok");
        }
    }
}
