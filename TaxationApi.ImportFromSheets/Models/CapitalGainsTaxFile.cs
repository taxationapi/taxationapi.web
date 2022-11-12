using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxationApi.ImportFromSheets.Models
{
    public class CapitalGainsTaxFile : BaseTaxationFile
    {
        public decimal Capital_gains_tax { get; set; }
        public decimal? Capital_gains_tax_non_res { get; set; }

        public DateTime? Capital_gains_tax_lastupdate { get; set; }
    }
}
