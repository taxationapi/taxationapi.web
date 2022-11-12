using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxationApi.Backend.Data;
using TaxationApi.Backend.Model.Taxation;

namespace TaxationApi.Backend.Services
{
  
    public class TaxationService : ITaxationService
    {
        
        public TaxationService()
        {
            
        }

        public List<TaxationData> GetTaxationData()
        {
            var database = Database.LoadData();
            return database;
        }
    }
}
