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

namespace stock_market.Controllers
{
    [Route("[controller]/[action]")]
    public class HistoricalStockController : ControllerBase
    {
        private readonly mainDB _db;

        public HistoricalStockController(mainDB db)
        {
            _db = db;
        }

        public string GetHistoricalPrice(string ticker)
        {

            if (ticker == null)
            {
                return "Please enter a ticker";
            }

            List<HistoricalStock> historicalStocks = _db.historicalStocks
                .Include(hs => hs.baseStock)
                .Where(hs => hs.baseStock.ticker == ticker)
                .ToList();

            //convert to json
            string json = JsonConvert.SerializeObject(historicalStocks);
            return json;


        }

    }
}