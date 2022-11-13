using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxationApi.ImportFromSheets.Models
{
    public class LumpsumpTaxationFile: BaseTaxationFile
    {
        public decimal Lumpsump_amount { get; set; }
        public decimal? Lumpsump_percentage { get; set; }
        public DateTime Lumpsump_dateupdated { get; set; }

    }
}
