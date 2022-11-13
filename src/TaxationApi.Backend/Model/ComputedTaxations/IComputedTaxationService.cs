using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxationApi.Backend.Model.ComputedTaxations.Requests;

namespace TaxationApi.Backend.Model.ComputedTaxations
{
    public interface IComputedTaxationService
    {
        List<ComputedTaxation> ComputeTaxations(ComputingTaxationRequest request);
    }
}
