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


        public ComputedIncomeTaxation? IncomeTaxation { get; set; }
        public ComputedIncomeTaxation? WealthTaxation { get; set; }
        public ComputedIncomeTaxation? CorporateTaxation { get; set; }
        public ComputedIncomeTaxation? CapitalGainsTaxation { get; set; }



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
                decimal monthTax = 0.0m;

                if (IncomeTaxation != null)
                {
                    monthTax += IncomeTaxation.MonthlyTax;
                }
                if (WealthTaxation != null)
                {
                    monthTax += WealthTaxation.MonthlyTax;
                }
                if (CorporateTaxation != null)
                {
                    monthTax += CorporateTaxation.MonthlyTax;
                }
                if (CapitalGainsTaxation != null)
                {
                    monthTax += CapitalGainsTaxation.MonthlyTax;
                }
                
                return monthTax;
            }
        }

        public decimal MonthlyGrossIncome
        {
            get
            {
                decimal monthGross = 0.0m;

                if (IncomeTaxation != null)
                {
                    monthGross += IncomeTaxation.MonthlyGrossIncome;
                }
                if (WealthTaxation != null)
                {
                    monthGross += WealthTaxation.MonthlyGrossIncome;
                }
                if (CorporateTaxation != null)
                {
                    monthGross += CorporateTaxation.MonthlyGrossIncome;
                }
                if (CapitalGainsTaxation != null)
                {
                    monthGross += CapitalGainsTaxation.MonthlyGrossIncome;
                }


                return monthGross;

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
