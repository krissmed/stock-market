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
    public class StockController : ControllerBase
    {
        private readonly mainDB _db;
        private const string date_from = "2022-10-18";
        private const string URL = "https://api.marketstack.com/v1/intraday";
        private const string parameters_before_sym = "?access_key=cb0917fe9d7a54323143c281fa427aa2&symbols=";
        private const string parameters_after_sym = "&interval=5min&date_from=" + date_from + "&date_to=";
        

        public StockController(mainDB db)
        {
            _db = db;
        }

        public bool AddStock(string ticker)
        {
            try
            {
                if (_db.baseStocks.Any(s => s.ticker == ticker))
                {
                    return false;
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(URL);
                string date_to = DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                string param = parameters_before_sym + ticker + parameters_after_sym + date_to;
                HttpResponseMessage response = client.GetAsync(param).Result;

                if (response.IsSuccessStatusCode)
                {

                    var body = response.Content.ReadAsStringAsync().Result;
                    Root api_returns = JsonConvert.DeserializeObject<Root>(body);
                    int total_pages = (int)Math.Ceiling((double)api_returns.pagination.total / 10000);

                    for (int i = 0; i < total_pages; i++)
                    {
                        param = parameters_before_sym + ticker + parameters_after_sym + date_to + "&limit=10000&offset=" + i;
                        Console.WriteLine("USING: " + param);
                        response = client.GetAsync(param).Result;
                        body = response.Content.ReadAsStringAsync().Result;
                        api_returns = JsonConvert.DeserializeObject<Root>(body);

                        foreach (var j in api_returns.data)
                        {
                            HistoricalStock ex_stock = new HistoricalStock();

                            //if j.last is null, use open price as ex_stock.price, otherwise, use last price
                            if (j.last == null)
                            {
                                ex_stock.price = (double)j.open;
                            }
                            else
                            {
                                ex_stock.price = Convert.ToDouble(j.last, CultureInfo.InvariantCulture);
                            }

                            Timestamp ts = _db.timestamps.Where(t => t.time == j.date).FirstOrDefault();

                            if (ts == null)
                            {
                                Timestamp timestamp = new Timestamp();
                                timestamp.time = j.date;
                                timestamp.unix = (int)j.date.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                                _db.timestamps.Add(timestamp);
                                _db.SaveChanges();

                            }

                            ex_stock.timestamp = ts;

                            //if there is not a stock with the same ticker in basestock, add it to the database

                            BaseStock base_stock = _db.baseStocks.Where(s => s.ticker == ticker).FirstOrDefault();
                            if (base_stock == null)
                            {
                                base_stock = new BaseStock();
                                base_stock.ticker = ticker;
                                base_stock.current_price = ex_stock.price;
                                _db.baseStocks.Add(base_stock);
                                _db.SaveChanges();

                            }

                            ex_stock.baseStock = base_stock;
                            _db.historicalStocks.Add(ex_stock);
                        }
                        _db.SaveChanges();

                    }

                    _db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public List<BaseStock> GetStocks()
        {
            return _db.baseStocks.ToList();
        }

        public bool DeleteStock(string ticker)
        {
            try
            {

                Console.WriteLine("Deleting stock with ticker: " + ticker);
                BaseStock stock = _db.baseStocks.Where(s => s.ticker == ticker).FirstOrDefault();
                if (stock == null)
                {
                    return false;
                }

                List<HistoricalStock> historicalStocks = _db.historicalStocks.Where(s => s.baseStock == stock).ToList();
                foreach (var i in historicalStocks)
                {
                    _db.historicalStocks.Remove(i);
                    _db.SaveChanges();
                }


                _db.baseStocks.Remove(stock);
                _db.SaveChanges();


                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}