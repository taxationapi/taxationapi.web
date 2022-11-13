using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxationApi.Backend.Model.Taxation;

namespace TaxationApi.Backend.Data
{
    public class TaxationDatabaseModel
    {
        public List<TaxationData> Taxations { get; set; }

        public TaxationDatabaseModel()
        {
            Taxations = new List<TaxationData>();
        }
    }
}
