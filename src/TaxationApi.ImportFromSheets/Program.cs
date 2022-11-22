
using CsvHelper;
using System.Globalization;
using System.Runtime.CompilerServices;
using CsvHelper.TypeConversion;
using TaxationApi.ImportFromSheets;
using TaxationApi.Web.Model.TaxRates;
using System.Dynamic;
using Newtonsoft.Json;
using TaxationApi.Backend.Model.Taxations;

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
                        if (rateStr == "?")
                            rateStr = -1m;

                        country.CapitalGainsTax = new TaxationOverViewEntityCapitalGainsViewModel()
                        {
                            Rate = Convert.ToDecimal(rateStr),
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

                        if (DoesPropertyExist(record, "Bracket_1_low"))
                        {
                            var lowAmountStr = record.Bracket_1_low;
                            if (!string.IsNullOrWhiteSpace(lowAmountStr))
                            {
                                var highAmountStr = record.Bracket_1_high;
                                var bracketStr = record.Bracket_1_amount.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, "");

                                var bracket = new TaxationBracketEntityViewModel()
                                {
                                    Rate = decimal.Parse(bracketStr),
                                    LowerBracket = decimal.Parse(lowAmountStr),
                                    HigherBracket = decimal.Parse(highAmountStr)
                                };
                                country.CorporateTax.Brackets.Add(bracket);
                            }


                        }
                        if (DoesPropertyExist(record, "Bracket_2_low"))
                        {
                            var lowAmountStr = record.Bracket_2_low;
                            if (!string.IsNullOrWhiteSpace(lowAmountStr))
                            {
                                var highAmountStr = record.Bracket_2_high;
                                var bracketStr = record.Bracket_2_amount.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, "");

                                var bracket = new TaxationBracketEntityViewModel()
                                {
                                    Rate = decimal.Parse(bracketStr),
                                    LowerBracket = decimal.Parse(lowAmountStr),
                                    HigherBracket = decimal.Parse(highAmountStr)
                                };
                                country.CorporateTax.Brackets.Add(bracket);
                            }


                        }
                        if (DoesPropertyExist(record, "Bracket_3_low"))
                        {
                            var lowAmountStr = record.Bracket_3_low;
                            if (!string.IsNullOrWhiteSpace(lowAmountStr))
                            {
                                var highAmountStr = record.Bracket_3_high;
                                var bracketStr = record.Bracket_3_amount.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, "");

                                var bracket = new TaxationBracketEntityViewModel()
                                {
                                    Rate = decimal.Parse(bracketStr),
                                    LowerBracket = decimal.Parse(lowAmountStr),
                                    HigherBracket = decimal.Parse(highAmountStr)
                                };
                                country.CorporateTax.Brackets.Add(bracket);
                            }


                        }
                        if (DoesPropertyExist(record, "Bracket_4_low"))
                        {
                            var lowAmountStr = record.Bracket_4_low;
                            if (!string.IsNullOrWhiteSpace(lowAmountStr))
                            {
                                var highAmountStr = record.Bracket_4_high;
                                var bracketStr = record.Bracket_4_amount.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, "");

                                var bracket = new TaxationBracketEntityViewModel()
                                {
                                    Rate = decimal.Parse(bracketStr),
                                    LowerBracket = decimal.Parse(lowAmountStr),
                                    HigherBracket = decimal.Parse(highAmountStr)
                                };
                                country.CorporateTax.Brackets.Add(bracket);
                            }



                        }
                    }
                }
            }
            if (type == InputFileType.IncomeTax)
            {
                if (DoesPropertyExist(record, "Income_tax"))
                {
                    var rateStr = record.Income_tax.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, "");

                    decimal amount = -1;
                    decimal.TryParse(rateStr, out amount);
                    
                    if (amount > -1)
                    {
                        country.IncomeTax = new TaxationOverViewEntityIncomeViewModel()
                        {
                            Rate = amount,
                            LastUpdated = DateTime.Now
                        };

                        if (DoesPropertyExist(record, "Bracket_1_low"))
                        {
                            var lowAmountStr = record.Bracket_1_low;
                            if (!string.IsNullOrWhiteSpace(lowAmountStr))
                            {
                                var highAmountStr = record.Bracket_1_high;
                                var bracketStr = record.Bracket_1_amount.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, "");

                                var bracket = new TaxationBracketEntityViewModel()
                                {
                                    Rate = decimal.Parse(bracketStr),
                                    LowerBracket = decimal.Parse(lowAmountStr),
                                    HigherBracket = decimal.Parse(highAmountStr)
                                };
                                country.IncomeTax.Brackets.Add(bracket);
                            }
                            
                          
                        }
                        if (DoesPropertyExist(record, "Bracket_2_low"))
                        {
                            var lowAmountStr = record.Bracket_2_low;
                            if (!string.IsNullOrWhiteSpace(lowAmountStr))
                            {
                                var highAmountStr = record.Bracket_2_high;
                                var bracketStr = record.Bracket_2_amount.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, "");

                                var bracket = new TaxationBracketEntityViewModel()
                                {
                                    Rate = decimal.Parse(bracketStr),
                                    LowerBracket = decimal.Parse(lowAmountStr),
                                    HigherBracket = decimal.Parse(highAmountStr)
                                };
                                country.IncomeTax.Brackets.Add(bracket);
                            }
                           

                        }
                        if (DoesPropertyExist(record, "Bracket_3_low"))
                        {
                            var lowAmountStr = record.Bracket_3_low;
                            if (!string.IsNullOrWhiteSpace(lowAmountStr))
                            {
                                var highAmountStr = record.Bracket_3_high;
                                var bracketStr = record.Bracket_3_amount.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, "");

                                var bracket = new TaxationBracketEntityViewModel()
                                {
                                    Rate = decimal.Parse(bracketStr),
                                    LowerBracket = decimal.Parse(lowAmountStr),
                                    HigherBracket = decimal.Parse(highAmountStr)
                                };
                                country.IncomeTax.Brackets.Add(bracket);
                            }
                            
                           
                        }
                        if (DoesPropertyExist(record, "Bracket_4_low"))
                        {
                            var lowAmountStr = record.Bracket_4_low;
                            if (!string.IsNullOrWhiteSpace(lowAmountStr))
                            {
                                var highAmountStr = record.Bracket_4_high;
                                var bracketStr = record.Bracket_4_amount.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, "");

                                var bracket = new TaxationBracketEntityViewModel()
                                {
                                    Rate = decimal.Parse(bracketStr),
                                    LowerBracket = decimal.Parse(lowAmountStr),
                                    HigherBracket = decimal.Parse(highAmountStr)
                                };
                                country.IncomeTax.Brackets.Add(bracket);
                            }
                                
                            
                          
                        }
                        if (DoesPropertyExist(record, "Bracket_5_low"))
                        {
                            var lowAmountStr = record.Bracket_5_low;
                            if (!string.IsNullOrWhiteSpace(lowAmountStr))
                            {
                                var highAmountStr = record.Bracket_5_high;
                                var bracketStr = record.Bracket_5_amount.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, "");

                                var bracket = new TaxationBracketEntityViewModel()
                                {
                                    Rate = decimal.Parse(bracketStr),
                                    LowerBracket = decimal.Parse(lowAmountStr),
                                    HigherBracket = decimal.Parse(highAmountStr)
                                };
                                country.IncomeTax.Brackets.Add(bracket);
                            }
                        }
                        if (DoesPropertyExist(record, "Bracket_6_low"))
                        {
                            var lowAmountStr = record.Bracket_6_low;
                            if (!string.IsNullOrWhiteSpace(lowAmountStr))
                            {
                                var highAmountStr = record.Bracket_6_high;
                                var bracketStr = record.Bracket_6_amount.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, "");

                                var bracket = new TaxationBracketEntityViewModel()
                                {
                                    Rate = decimal.Parse(bracketStr),
                                    LowerBracket = decimal.Parse(lowAmountStr),
                                    HigherBracket = decimal.Parse(highAmountStr)
                                };
                                country.IncomeTax.Brackets.Add(bracket);
                            }
                        }
                        if (DoesPropertyExist(record, "Bracket_7_low"))
                        {
                            var lowAmountStr = record.Bracket_7_low;
                            if (!string.IsNullOrWhiteSpace(lowAmountStr))
                            {
                                var highAmountStr = record.Bracket_7_high;
                                var bracketStr = record.Bracket_7_amount.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, "");

                                var bracket = new TaxationBracketEntityViewModel()
                                {
                                    Rate = decimal.Parse(bracketStr),
                                    LowerBracket = decimal.Parse(lowAmountStr),
                                    HigherBracket = decimal.Parse(highAmountStr)
                                };
                                country.IncomeTax.Brackets.Add(bracket);
                            }
                        }
                        if (DoesPropertyExist(record, "Bracket_8_low"))
                        {
                            var lowAmountStr = record.Bracket_8_low;
                            if (!string.IsNullOrWhiteSpace(lowAmountStr))
                            {
                                var highAmountStr = record.Bracket_8_high;
                                var bracketStr = record.Bracket_8_amount.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, "");

                                var bracket = new TaxationBracketEntityViewModel()
                                {
                                    Rate = decimal.Parse(bracketStr),
                                    LowerBracket = decimal.Parse(lowAmountStr),
                                    HigherBracket = decimal.Parse(highAmountStr)
                                };
                                country.IncomeTax.Brackets.Add(bracket);
                            }
                        }
                        if (DoesPropertyExist(record, "Bracket_9_low"))
                        {
                            var lowAmountStr = record.Bracket_9_low;
                            if (!string.IsNullOrWhiteSpace(lowAmountStr))
                            {
                                var highAmountStr = record.Bracket_9_high;
                                var bracketStr = record.Bracket_9_amount.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, "");

                                var bracket = new TaxationBracketEntityViewModel()
                                {
                                    Rate = decimal.Parse(bracketStr),
                                    LowerBracket = decimal.Parse(lowAmountStr),
                                    HigherBracket = decimal.Parse(highAmountStr)
                                };
                                country.IncomeTax.Brackets.Add(bracket);
                            }
                        }
                        if (DoesPropertyExist(record, "Bracket_10_low"))
                        {
                            var lowAmountStr = record.Bracket_10_low;
                            if (!string.IsNullOrWhiteSpace(lowAmountStr))
                            {
                                var highAmountStr = record.Bracket_10_high;
                                var bracketStr = record.Bracket_10_amount.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, "");

                                var bracket = new TaxationBracketEntityViewModel()
                                {
                                    Rate = decimal.Parse(bracketStr),
                                    LowerBracket = decimal.Parse(lowAmountStr),
                                    HigherBracket = decimal.Parse(highAmountStr)
                                };
                                country.IncomeTax.Brackets.Add(bracket);
                            }
                        }

                    }
                }
            }
            if (type == InputFileType.WealthTax)
            {
                if (DoesPropertyExist(record, "Rate"))
                {
                    var rateStr = record.Rate.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, "");

                    decimal amount = -1;
                    decimal.TryParse(rateStr, out amount);
                    

                    if (!string.IsNullOrWhiteSpace(rateStr)&&amount > -1)
                    {
                        country.WealthTax = new TaxationOverViewEntityWealthTaxViewModel()
                        {
                            Rate = amount,
                            Base = Decimal.Parse(record.Base),
                            Comments = record.Comments,
                            LastUpdated = DateTime.Now
                        };
                    }
                }
            }
            if (type == InputFileType.LumpsumpTax)
            {
                if (DoesPropertyExist(record, "Lumpsump_amount"))
                {
                    decimal amount = -1;
                    decimal.TryParse(record.Lumpsump_amount.ToString(), out amount);

                    if (amount > 0)
                    {
                        country.LumpsumpTax = new TaxationOverViewEntityLumpSumpViewModel()
                        {
                            Amount = amount,
                            LastUpdated = DateTime.Now
                        };
                    }
                }
            }
        }



    }
}

foreach(var capitalGains in model.Taxations.Where(c=>c.CapitalGainsTax != null && c.CapitalGainsTax.Rate == -1))
{
    var incomeTaxForCountry = model.Taxations.FirstOrDefault(c => c.Alpha3 == capitalGains.Alpha3);
    if (incomeTaxForCountry != null)
    {
        capitalGains.CapitalGainsTax.Rate = incomeTaxForCountry.IncomeTax.Rate;
        capitalGains.CapitalGainsTax.ValidationLevel = ValidationLevel.Guess;
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
    if (name.Contains("Lump"))
        return InputFileType.LumpsumpTax;
    if (name.Contains("Wealth-tax"))
        return InputFileType.WealthTax;
    
    return null;
}

static bool DoesPropertyExist(dynamic settings, string name)
{
    if (settings is ExpandoObject)
        return ((IDictionary<string, object>)settings).ContainsKey(name);

    return settings.GetType().GetProperty(name) != null;
}