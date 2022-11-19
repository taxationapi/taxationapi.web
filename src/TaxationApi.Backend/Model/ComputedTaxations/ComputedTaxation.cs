using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxationApi.Backend.Model.ComputedTaxations
{
    public class ComputedTaxation
    {
        public string Name { get; set; }
        public string Alpha2 { get; set; }
        public string Alpha3 { get; set; }
        public bool IsAllDataAvailable { get; set; }

    
        public ComputedIncomeTaxation IncomeTaxation { get; set; }

        public decimal MonthlyNetIncome
        {
            get
            {
                return MonthlyGrossIncome - MonthlyTax;
            }
        }

        public decimal MonthlyTax
        {
            get
            {
                return IncomeTaxation.MonthlyTax;
            }
        }

        public decimal MonthlyGrossIncome
        {
            get
            {
                return IncomeTaxation.MonthlyGrossIncome;

            }
        }

        public decimal YearlyNetIncome
        {
            get
            {
                return MonthlyNetIncome * 12;
            }
        }

        public decimal YearlyTax
        {
            get
            {
                return MonthlyTax * 12;
            }
        }

        public decimal YearlyGrossIncome
        {
            get
            {
                return YearlyNetIncome + YearlyTax;
            }
        }

        public decimal EffectiveTaxPercentage
        {
            get
            {
                if (YearlyGrossIncome == 0)
                {
                    return 0;
                }

                return 1 - YearlyNetIncome / YearlyGrossIncome;
            }
        }

        public ComputedTaxation()
        {
            
        }
    }

    public class ComputedIncomeTaxation
    {
        public decimal MonthlyGrossIncome { get; set; }
        public decimal MonthlyTax { get; set; }
        public List<ComputedTaxBracket> Brackets { get; set; }
        public ComputedIncomeTaxation()
        {
            Brackets = new List<ComputedTaxBracket>();
        }
    }
}
