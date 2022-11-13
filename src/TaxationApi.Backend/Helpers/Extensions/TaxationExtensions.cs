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

            if (taxationData.IncomeTax != null)
            {
                decimal monthlyIncomeTax = (request.YearlyIncome / 12) * (taxationData.IncomeTax.Rate/100);
                totalMonthlyTax += monthlyIncomeTax;
            }
            else
            {
                isAllData = false;
            }

            if (taxationData.CorporateTax != null)
            {
                decimal monthlyCorporateTax = (request.YearlyCorporateProfits / 12) * (taxationData.CorporateTax.Rate / 100);
                totalMonthlyTax += monthlyCorporateTax;
            }
            else
            {
                isAllData = false;
            }

            if (taxationData.CapitalGainsTax != null)
            {
                decimal monthlyCapitalGainsTax = (request.YearlyCapitalGains / 12) * (taxationData.CapitalGainsTax.Rate / 100);
                totalMonthlyTax += monthlyCapitalGainsTax;
            }
            else
            {
                isAllData = false;
            }

            taxation.MonthlyTax = totalMonthlyTax;
            taxation.MonthlyNetIncome =
                ((request.YearlyIncome + request.YearlyCorporateProfits + request.YearlyCapitalGains) / 12) -
                totalMonthlyTax;
            taxation.IsAllDataAvailable = isAllData;

            return taxation;


        }
    }
}
