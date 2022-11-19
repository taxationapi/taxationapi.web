using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxationApi.Backend.Model.ComputedTaxations;
using TaxationApi.Backend.Model.ComputedTaxations.Requests;
using TaxationApi.Backend.Model.Taxation;

namespace TaxationApi.Backend.Helpers.Extensions
{
    public static class TaxationExtensions
    {
        public static ComputedIncomeTaxation GetIncomeTax(this TaxationData taxationData, ComputingTaxationRequest request, decimal rate)
        {
            ComputedIncomeTaxation incomeTaxation = new ComputedIncomeTaxation();
            var incomeTax = taxationData.IncomeTax;
            
            if (incomeTax != null)
            {
                var taxableIncomeUsd = request.YearlyIncome;  // we always convert to local as the brackets should be in local currency
                var taxableIncome = taxableIncomeUsd*rate;
                var taxedIncomeSoFar = 0.0m;
                var taxabaleIncomeLeft = taxableIncome - taxedIncomeSoFar;
                var lowestBracket = 0.0m;
                foreach (var incomeBracket in incomeTax.Brackets.OrderBy(c=>c.LowerBracket))
                {
                    lowestBracket = incomeBracket.LowerBracket;
                    
                    if (taxabaleIncomeLeft > 0) // we need to take income from this bracket
                    {
                        bool shouldUseEntireBracket = taxabaleIncomeLeft > incomeBracket.BracketSize;
                        if (shouldUseEntireBracket)
                        {
                            var taxableIncomeInBracket = incomeBracket.BracketSize * incomeBracket.Rate.Value / 100m;
                            incomeTaxation.Brackets.Add(new ComputedTaxBracket()
                            {
                                LowerBracket = incomeBracket.LowerBracket,
                                HigherBracket = incomeBracket.HigherBracket,
                                TaxInBracket = taxableIncomeInBracket,
                                Rate = incomeBracket.Rate.Value,
                                IncomeInBracket = incomeBracket.BracketSize
                            });
                            taxedIncomeSoFar += incomeBracket.BracketSize;
                            taxabaleIncomeLeft -= incomeBracket.BracketSize;
                        }
                        else
                        {
                            var taxableAmountInBracket = taxabaleIncomeLeft;
                            var taxableIncomeBracket = taxableAmountInBracket * incomeBracket.Rate.Value / 100m;
                            incomeTaxation.Brackets.Add(new ComputedTaxBracket()
                            {
                                LowerBracket = incomeBracket.LowerBracket,
                                HigherBracket = incomeBracket.HigherBracket,
                                TaxInBracket = taxableIncomeBracket,
                                Rate = incomeBracket.Rate.Value,
                                IncomeInBracket = taxableAmountInBracket
                            });
                            taxedIncomeSoFar += taxableAmountInBracket;
                            taxabaleIncomeLeft = 0;
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                    
                }

                if (taxabaleIncomeLeft > 0) // rest income should be the full taxation amount
                {
                    var taxableAmountOutsideBracket = taxabaleIncomeLeft * incomeTax.Rate / 100m;
                    incomeTaxation.Brackets.Add(new ComputedTaxBracket()
                    {
                        LowerBracket = lowestBracket,
                        TaxInBracket = taxableAmountOutsideBracket,
                        Rate = incomeTax.Rate,
                        IncomeInBracket = taxabaleIncomeLeft
                    });
                }
            }
            else
            {
                return null;
            }

            // now we need to convert it back to USD
            foreach (var bracket in incomeTaxation.Brackets)
            {
                bracket.LowerBracket = bracket.LowerBracket / rate;
                bracket.HigherBracket = bracket.HigherBracket / rate;
                bracket.TaxInBracket = bracket.TaxInBracket / rate;
                bracket.IncomeInBracket = bracket.IncomeInBracket / rate;
            }

            var localMonthlyTax = incomeTaxation.Brackets.Sum(c => c.TaxInBracket) / 12;
            var localMonthlyNet = (request.YearlyIncome / 12) - incomeTaxation.MonthlyTax;
            incomeTaxation.MonthlyTax = localMonthlyTax;
            incomeTaxation.MonthlyNetIncome = localMonthlyNet;

            return incomeTaxation;
        }

        //public static ComputedTaxation GetTax(this TaxationData taxationData, ComputingTaxationRequest request)
        //{
        //    ComputedTaxation taxation = new ComputedTaxation()
        //    {
        //        Alpha2 = taxationData.Alpha2,
        //        Alpha3 = taxationData.Alpha3,
        //        Name = taxationData.Name
        //    };

        //    bool isAllData = true;

        //    decimal totalMonthlyTax = 0.0m;
            
        //    totalMonthlyTax = CalculateCorporateTax(taxationData, request, totalMonthlyTax, ref isAllData);
        //    totalMonthlyTax = CalculateCapitalGainsTax(taxationData, request, totalMonthlyTax, ref isAllData);
        //    totalMonthlyTax = CalculateWealthTax(taxationData, request, totalMonthlyTax, ref isAllData);
            
        //   // SetTaxationObject(request, taxation, totalMonthlyTax, isAllData);

        //    return taxation;


        //}
        

        //private static void SetTaxationObject(ComputingTaxationRequest request, ComputedTaxation taxation,
        //    decimal totalMonthlyTax, bool isAllData)
        //{
        //    taxation.MonthlyTax = totalMonthlyTax;
        //    taxation.MonthlyNetIncome =
        //        ((request.YearlyIncome + request.YearlyCorporateProfits + request.YearlyCapitalGains) / 12) -
        //        totalMonthlyTax;
        //    taxation.IsAllDataAvailable = isAllData;
        //}

        private static decimal CalculateWealthTax(TaxationData taxationData, ComputingTaxationRequest request,
            decimal totalMonthlyTax, ref bool isAllData)
        {
            if (taxationData.WealthTax != null && request.TotalWealth.HasValue && request.TotalWealth.Value > 0)
            {
                var minimumBase = taxationData.WealthTax.Base;
                if (request.TotalWealth > minimumBase)
                {
                    var amountAbove = request.TotalWealth - minimumBase;
                    var wealthTaxation = amountAbove * (taxationData.WealthTax.Rate/100);
                    var wealthTaxationMonthly = wealthTaxation / 12;
                    
                    totalMonthlyTax += wealthTaxationMonthly.Value;

                }
            }

            return totalMonthlyTax;
        }

        
        private static decimal CalculateCapitalGainsTax(TaxationData taxationData, ComputingTaxationRequest request,
            decimal totalMonthlyTax, ref bool isAllData)
        {
            if (taxationData.CapitalGainsTax != null)
            {
                decimal monthlyCapitalGainsTax = (request.YearlyCapitalGains / 12) * (taxationData.CapitalGainsTax.Rate / 100);
                totalMonthlyTax += monthlyCapitalGainsTax;
            }
            else
            {
                isAllData = false;
            }

            return totalMonthlyTax;
        }

        private static decimal CalculateCorporateTax(TaxationData taxationData, ComputingTaxationRequest request,
            decimal totalMonthlyTax, ref bool isAllData)
        {
            if (taxationData.CorporateTax != null)
            {
                decimal monthlyCorporateTax = (request.YearlyCorporateProfits / 12) * (taxationData.CorporateTax.Rate / 100);
                totalMonthlyTax += monthlyCorporateTax;
            }
            else
            {
                isAllData = false;
            }

            return totalMonthlyTax;
        }

        private static decimal CalculateIncomeTax(TaxationData taxationData, ComputingTaxationRequest request,
            decimal totalMonthlyTax, ref bool isAllData)
        {
            if (taxationData.IncomeTax != null)
            {
                decimal monthlyIncomeTax = (request.YearlyIncome / 12) * (taxationData.IncomeTax.Rate / 100);
                totalMonthlyTax += monthlyIncomeTax;
            }
            else
            {
                isAllData = false;
            }

            return totalMonthlyTax;
        }
    }
}
