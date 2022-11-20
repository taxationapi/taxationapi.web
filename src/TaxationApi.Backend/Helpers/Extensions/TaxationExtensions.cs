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
            
            if (incomeTax != null && request.YearlyIncome > 0)
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
            var localMonthlyGross = (request.YearlyIncome / 12) - incomeTaxation.MonthlyTax;
            incomeTaxation.MonthlyTax = localMonthlyTax;
            incomeTaxation.MonthlyGrossIncome = localMonthlyGross;

            return incomeTaxation;
        }


        public static ComputedIncomeTaxation GetCorporateTax(this TaxationData taxationData, ComputingTaxationRequest request, decimal rate)
        {
            ComputedIncomeTaxation incomeTaxation = new ComputedIncomeTaxation();
            var incomeTax = taxationData.CorporateTax;

            if (incomeTax != null && request.YearlyCorporateProfits > 0)
            {
                var taxableIncomeUsd = request.YearlyCorporateProfits;  // we always convert to local as the brackets should be in local currency
                var taxableIncome = taxableIncomeUsd * rate;
                var taxedIncomeSoFar = 0.0m;
                var taxabaleIncomeLeft = taxableIncome - taxedIncomeSoFar;
                var lowestBracket = 0.0m;
                foreach (var incomeBracket in incomeTax.Brackets.OrderBy(c => c.LowerBracket))
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
            var localMonthlyGross = (request.YearlyCorporateProfits / 12) - incomeTaxation.MonthlyTax;
            incomeTaxation.MonthlyTax = localMonthlyTax;
            incomeTaxation.MonthlyGrossIncome = localMonthlyGross;

            return incomeTaxation;
        }



        public static ComputedIncomeTaxation GetWealthTax(this TaxationData taxationData, ComputingTaxationRequest request, decimal rate)
        {
            ComputedIncomeTaxation incomeTaxation = new ComputedIncomeTaxation();
            var wealthTax = taxationData.WealthTax;

            if (wealthTax != null && request.TotalWealth.HasValue && request.TotalWealth.Value > 0)
            {
                var taxableIncomeUsd = request.TotalWealth;  // we always convert to local as the brackets should be in local currency
                var taxableIncome = taxableIncomeUsd * rate;
                var baseStart = wealthTax.Base;
                var taxedIncomeSoFar = 0.0m;
                var lowestBracket = 0.0m;
            
                if (taxableIncome>baseStart)
                {
                    var amountToTax = taxableIncome - baseStart;
                    var taxableAmountOutsideBracket = amountToTax * (wealthTax.Rate / 100m);
                    
                    incomeTaxation.Brackets.Add(new ComputedTaxBracket()
                    {
                        LowerBracket = baseStart,
                        TaxInBracket = taxableAmountOutsideBracket.Value,
                        Rate = wealthTax.Rate,
                        IncomeInBracket = amountToTax.Value
                    });
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

            foreach (var bracket in incomeTaxation.Brackets)
            {
                bracket.LowerBracket = bracket.LowerBracket / rate;
                bracket.HigherBracket = bracket.HigherBracket / rate;
                bracket.TaxInBracket = bracket.TaxInBracket / rate;
                bracket.IncomeInBracket = bracket.IncomeInBracket / rate;
            }

            var localMonthlyTax = incomeTaxation.Brackets.Sum(c => c.TaxInBracket) / 12;
            var localMonthlyGross = (request.TotalWealth.Value / 12) - incomeTaxation.MonthlyTax;
            incomeTaxation.MonthlyTax = localMonthlyTax;
            incomeTaxation.MonthlyGrossIncome = localMonthlyGross;

            return incomeTaxation;
        }
        
        
    }
}
