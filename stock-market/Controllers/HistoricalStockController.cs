using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using stock_market.Model;
using stock_market.DAL;
using System.Diagnostics.CodeAnalysis;

namespace stock_market.Controllers
{
    [Route("[controller]/[action]")]
    [ExcludeFromCodeCoverage]
    public class HistoricalStockController : ControllerBase
    {
        private readonly IHistoricalStockRepository _db;

        public HistoricalStockController(IHistoricalStockRepository db)
        {
            _db = db;
        }

        public async Task<string> GetHistoricalPrice(string ticker)
        {
            return await _db.GetHistoricalPrice(ticker);
        }


    }
}