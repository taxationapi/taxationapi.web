using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxationApi.Backend.Model.ComputedTaxations
{
    public class ComputedTaxBracket
    {

        public decimal LowerBracket { get; set; }
        public decimal? HigherBracket { get; set; }
        public decimal TaxInBracket { get; set; }
        public decimal IncomeInBracket { get; set; }
        public decimal Rate { get; set; }

        public decimal? BracketSize
        {
            get
            {
                if (HigherBracket.HasValue)
                {
                    return HigherBracket.Value - LowerBracket;
                }

                return null;
            }
        }
        
        
        public decimal? BracketPercentUsed
        {
            get
            {
                if (BracketSize.HasValue)
                {
                    if (BracketSize.Value == 0)
                    {
                        return 100;
                    }

                    return (IncomeInBracket / BracketSize.Value)*100;
                }

                return null;
            }

        }
    }
}
