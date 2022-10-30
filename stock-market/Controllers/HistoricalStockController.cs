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
            //time this function
            var watch = System.Diagnostics.Stopwatch.StartNew();
            BaseStock stock = _db.baseStocks.Where(s => s.ticker == ticker).FirstOrDefault();

            //get all entries where in historicalstocks where h.baseStock == stock
            List<HistoricalStock> historicalstock = _db.historicalStocks.Where(h => h.baseStock == stock).Include(h => h.timestamp).ToList();

            //sort the list by timestamp
            historicalstock.Sort((x, y) => x.timestamp.time.CompareTo(y.timestamp.time));

            string json = JsonConvert.SerializeObject(historicalstock);

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine(elapsedMs.ToString());


            return json;


        }


    }
}