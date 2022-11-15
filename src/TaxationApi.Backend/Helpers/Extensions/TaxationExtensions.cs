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
        public static ComputedTaxation GetTax(this TaxationData taxationData, ComputingTaxationRequest request)
        {
            ComputedTaxation taxation = new ComputedTaxation()
            {
                Alpha2 = taxationData.Alpha2,
                Alpha3 = taxationData.Alpha3,
                Name = taxationData.Name
            };

            bool isAllData = true;

            decimal totalMonthlyTax = 0.0m;

            totalMonthlyTax = CalculateIncomeTax(taxationData, request, totalMonthlyTax, ref isAllData);
            totalMonthlyTax = CalculateCorporateTax(taxationData, request, totalMonthlyTax, ref isAllData);
            totalMonthlyTax = CalculateCapitalGainsTax(taxationData, request, totalMonthlyTax, ref isAllData);
            totalMonthlyTax = CalculateWealthTax(taxationData, request, totalMonthlyTax, ref isAllData);
            
            SetTaxationObject(request, taxation, totalMonthlyTax, isAllData);

            return taxation;


        }
        

        private static void SetTaxationObject(ComputingTaxationRequest request, ComputedTaxation taxation,
            decimal totalMonthlyTax, bool isAllData)
        {
            taxation.MonthlyTax = totalMonthlyTax;
            taxation.MonthlyNetIncome =
                ((request.YearlyIncome + request.YearlyCorporateProfits + request.YearlyCapitalGains) / 12) -
                totalMonthlyTax;
            taxation.IsAllDataAvailable = isAllData;
        }

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
