﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxationApi.Backend.Model.ExchangeRates
{
   public interface IExchangeRateService
   {
       decimal Convert(decimal amount, string mainCurrency, string toCurrency);
   }
}
