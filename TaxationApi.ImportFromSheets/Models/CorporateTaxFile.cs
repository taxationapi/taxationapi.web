using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxationApi.ImportFromSheets.Models
{
    public class CorporateTaxFile : BaseTaxationFile
    {
        public decimal Corporate_tax { get; set; }
        public DateTime? Corporate_tax_lastupdate { get; set; }
    }
}
