﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxationApi.Backend.Data;

namespace TaxationApi.Backend.Model.Taxation
{
    public interface ITaxationService
    {
        List<CountryData> GetTaxationData();
    }
}
