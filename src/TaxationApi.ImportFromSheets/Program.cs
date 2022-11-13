
using CsvHelper;
using System.Globalization;
using System.Runtime.CompilerServices;
using CsvHelper.TypeConversion;
using TaxationApi.ImportFromSheets;
using TaxationApi.ImportFromSheets.Models;
using TaxationApi.Web.Model.TaxRates;
using System.Dynamic;
using Newtonsoft.Json;

var outputpath = "country_data.json";
var files = Directory.GetFiles("InputFiles");
TaxationOverviewViewModel model = new TaxationOverviewViewModel();

foreach (var file in files)
{
    var type = GetFileTypeBasedOnStringName(file);

    using (var reader = new StreamReader(file))
    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
    {
        var records = csv.GetRecords<dynamic>();
        foreach (var record in records)
        {
            var doesCountryExist = model.Taxations.Any(c => c.Alpha2 == record.Alpha2);
            if (!doesCountryExist)
            {
                model.Taxations.Add(new TaxationOverViewEntityViewModel() { Name = record.Name, Alpha2 = record.Alpha2, Alpha3 = record.Alpha3 });
            }

            var country = model.Taxations.FirstOrDefault(c => c.Alpha2 == record.Alpha2);
            
            if (type == InputFileType.CapitalGainsTax)
            {
                if (DoesPropertyExist(record, "Capital_gains_tax"))
                {
                    var rateStr = record.Capital_gains_tax.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, "");

                    if (!string.IsNullOrWhiteSpace(rateStr))
                    {
                        country.CapitalGainsTax = new TaxationOverViewEntityCapitalGainsViewModel()
                        {
                            Rate = decimal.Parse(rateStr),
                            LastUpdated = DateTime.Now
                        };
                    }
                }
            }
            if (type == InputFileType.CorporateTax)
            {
                if (DoesPropertyExist(record, "Corporate_tax"))
                {
                    var rateStr = record.Corporate_tax.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, "");

                    if (!string.IsNullOrWhiteSpace(rateStr))
                    {
                        country.CorporateTax = new TaxationOverViewEntityCorporateViewModel()
                        {
                            Rate = decimal.Parse(rateStr),
                            LastUpdated = DateTime.Now
                        };
                    }
                }
            }
            if (type == InputFileType.IncomeTax)
            {
        
            }
        }
    

 
    }
}

model.Taxations = model.Taxations.OrderBy(c => c.Name).ToList();
var dataToStore = JsonConvert.SerializeObject(model);
File.WriteAllText(outputpath, dataToStore);

InputFileType? GetFileTypeBasedOnStringName(string name)
{
    if (name.Contains("Capital gains"))
        return InputFileType.CapitalGainsTax;
    if (name.Contains("Corporate tax"))
        return InputFileType.CorporateTax;
    if (name.Contains("Income tax"))
        return InputFileType.IncomeTax;

    return null;
}

static bool DoesPropertyExist(dynamic settings, string name)
{
    if (settings is ExpandoObject)
        return ((IDictionary<string, object>)settings).ContainsKey(name);

    return settings.GetType().GetProperty(name) != null;
}