using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxationApi.Backend.Model.Taxations
{
    public enum ValidationLevel
    {
        Lookup,
        LookupInDepth,
        VerifiedByLocal,
        VerifiedByLawyer,
        Problematic
    }
}
