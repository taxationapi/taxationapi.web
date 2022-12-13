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



        public decimal MonthlyNetIncomeExcludingWealth
        {
            get
            {
                return MonthlyGrossIncomeExcludingWealth - MonthlyTaxExcludingWealth;
            }
        }

        public decimal MonthlyTaxExcludingWealth
        {
            get
            {
                decimal monthTax = 0.0m;

                if (IncomeTaxation != null)
                {
                    monthTax += IncomeTaxation.MonthlyTax;
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

        private decimal TotalNetworth
        {
            get
            {
                if (WealthTaxation != null)
                {
                    return WealthTaxation.MonthlyGrossIncome * 12;
                }

                return 0.0m;
            }
        }

        private decimal TotalNetworthTax
        {
            get
            {
                if (WealthTaxation != null)
                {
                    return WealthTaxation.MonthlyTax * 12;
                }

                return 0.0m;
            }
        }

        public decimal MonthlyGrossIncomeExcludingWealth
        {
            get
            {
                decimal monthGross = 0.0m;

                if (IncomeTaxation != null)
                {
                    monthGross += IncomeTaxation.MonthlyGrossIncome;
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

        public decimal YearlyNetIncomeExcludingWealth
        {
            get
            {
                return MonthlyNetIncomeExcludingWealth * 12;
            }
        }

        public decimal YearlyTaxExcludingWealth
        {
            get
            {
                return MonthlyTaxExcludingWealth * 12;
            }
        }

        public decimal YearlyGrossIncomeExcludingWealth
        {
            get
            {
                return YearlyNetIncomeExcludingWealth + YearlyTaxExcludingWealth;
            }
        }

        public decimal YearlyTotalTax
        {
            get
            {
                return YearlyTaxExcludingWealth + TotalNetworthTax;
            }
        }

        public decimal EffectiveIncomeTaxPercentage
        {
            get
            {
                if (YearlyGrossIncomeExcludingWealth == 0)
                {
                    return 0;
                }

                return 1 - YearlyNetIncomeExcludingWealth / YearlyGrossIncomeExcludingWealth;
            }
        }



        public decimal EffectiveNetworthTaxPercentage
        {
            get
            {
                if (TotalNetworth == 0)
                {
                    return 0;
                }

                return 1 - (TotalNetworth - TotalNetworthTax)  / TotalNetworth;
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
