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

        public async Task<string> GetHistoricalPrice(string ticker)
        {

            if (ticker == null)
            {
                var ret = "Please enter a ticker";
                return ret;
            }
            //rewrite the code under to use async await

            BaseStock stock = await _db.baseStocks.FirstAsync(s => s.ticker == ticker);


            //get all entries where in historicalstocks where h.baseStock == stock
            List<HistoricalStock> historicalstock = await _db.historicalStocks
                .Where(h => h.baseStock == stock)
                .Include(h => h.timestamp)
                .ToListAsync();

            //sort the list by timestamp
            historicalstock.Sort((x, y) => x.timestamp.time.CompareTo(y.timestamp.time));
            string json = JsonConvert.SerializeObject(historicalstock);
            return json;
        }
    }
}