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
        

        public StockController(mainDB db)
        {
            _db = db;
        }

        public async Task<bool> AddStock(string ticker)
        {
            try
            {
                List<HistoricalStock> all_historical_stocks = new List<HistoricalStock>();
                List<Timestamp> all_timestamps = await _db.timestamps
                    .ToListAsync();
                List<BaseStock> all_base_stocks = new List<BaseStock>();

                Timestamp earliest_timestamp = await _db.timestamps
                    .OrderBy(t => t.unix)
                    .FirstAsync();
                string date_from = earliest_timestamp.time.ToString("yyyy-MM-dd");
                string URL = "https://api.marketstack.com/v1/intraday";
                string parameters_before_sym = "?access_key=cb0917fe9d7a54323143c281fa427aa2&symbols=";
                string parameters_after_sym = "&interval=1min&date_from=" + date_from + "&date_to=";

                if (await _db.baseStocks.AnyAsync(s => s.ticker == ticker))
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

                            Timestamp ts = all_timestamps.Find(t => t.time == j.date);
                            ex_stock.timestamp = ts;

                            //if there is not a stock with the same ticker in basestock, add it to the database

                            BaseStock base_stock = all_base_stocks.Find(s => s.ticker == ticker);
                            if (base_stock == null)
                            {
                                base_stock = new BaseStock
                                {
                                    ticker = ticker,
                                    current_price = ex_stock.price
                                };
                                all_base_stocks.Add(base_stock);
                                await _db.baseStocks.AddAsync(base_stock);
                                await _db.SaveChangesAsync();
                            }
                            ex_stock.baseStock = base_stock;
                            all_historical_stocks.Add(ex_stock);
                        }

                    }
                    await _db.historicalStocks.AddRangeAsync(all_historical_stocks);
                    await _db.SaveChangesAsync();

                    //For every timestamp, check if the stock has a historicalStock object for that timestamp
                    Console.WriteLine("Filling historical stocks");

                    List<HistoricalStock> all_ffilled_historical_stocks = new List<HistoricalStock>();
                    all_historical_stocks = await _db.historicalStocks
                        .Where(s => s.baseStock.ticker == ticker)
                        .ToListAsync();
                    all_timestamps.Sort((x, y) => x.unix.CompareTo(y.unix));
                    BaseStock stock = await _db.baseStocks
                        .Where(s => s.ticker == ticker)
                        .FirstAsync();

                    foreach (var ts in all_timestamps)
                    {
                        //check if the historical stock exists in the list of historical stocks
                        if (!all_historical_stocks.Any(h => h.timestamp == ts))
                        {
                            HistoricalStock prev_hs = all_historical_stocks.Find(h => h.timestamp.unix == ts.unix - 60);

                            if (prev_hs != null)
                            {
                                HistoricalStock historical_stock = new HistoricalStock();
                                historical_stock.baseStock = stock;
                                historical_stock.timestamp = ts;
                                historical_stock.price = prev_hs.price;
                                all_ffilled_historical_stocks.Add(historical_stock);
                                all_historical_stocks.Add(historical_stock);
                            }
                        }
                    }
                    await _db.historicalStocks.AddRangeAsync(all_ffilled_historical_stocks);
                    await _db.SaveChangesAsync();

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

        public async Task<List<BaseStock>> GetStocks()
        {
            return await _db.baseStocks.ToListAsync();
        }

        public async Task<bool> DeleteStock(string ticker)
        {
            try
            {

                Console.WriteLine("Deleting stock with ticker: " + ticker);
                BaseStock stock = await _db.baseStocks
                    .Where(s => s.ticker == ticker)
                    .FirstOrDefaultAsync();
                
                if (stock == null)
                {
                    return false;
                }

                List<HistoricalStock> historicalStocks = await _db.historicalStocks
                    .Where(s => s.baseStock == stock)
                    .ToListAsync();
                
                _db.historicalStocks.RemoveRange(historicalStocks);
                _db.baseStocks.Remove(stock);
                await _db.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}