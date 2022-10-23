using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using stock_market.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using System.Security.Policy;



public class DbInitializer : IDbInitializer
{
    private readonly IServiceScopeFactory _scopeFactory;

    private List<string> tickers = new List<string>();

    private const string date_from = "2022-10-18";
    private const string URL = "https://api.marketstack.com/v1/intraday";
    private const string parameters_before_sym = "?access_key=cb0917fe9d7a54323143c281fa427aa2&symbols=";
    private const string parameters_after_sym = "&interval=5min&date_from=" + date_from + "&date_to=";


    public DbInitializer(IServiceScopeFactory scopeFactory)
    {
        this._scopeFactory = scopeFactory;
    }

    public void Initialize()
    {
        using (var serviceScope = _scopeFactory.CreateScope())
        {
            using (var context = serviceScope.ServiceProvider.GetService<mainDB>())
            {
                context.Database.Migrate();
            }
        }
    }

    public void InitializeTimestamps()
    {
        using (var serviceScope = _scopeFactory.CreateScope())
        {
            using (var _db = serviceScope.ServiceProvider.GetService<mainDB>())
            {
                // Create a Timestamp object for every minute since date_from
                DateTime date = DateTime.ParseExact(date_from, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                DateTime now = DateTime.Now;
                while (date < now)
                {
                    Timestamp timestamp = new Timestamp();
                    timestamp.time = date;
                    timestamp.unix = (int)date.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                    _db.timestamps.Add(timestamp);
                    date = date.AddMinutes(1);
                }
                _db.SaveChanges();
                Console.WriteLine("Timestamps added");
            }
        }
    }

    public void InitializeUser()
    {
        using (var serviceScope = _scopeFactory.CreateScope())
        {
            using (var _db = serviceScope.ServiceProvider.GetService<mainDB>())
            {
                //Create User
                User john_doe = new User();
                john_doe.curr_balance = 100_000;
                john_doe.curr_balance_liquid = 100_000;
                john_doe.curr_balance_stock = 0;
                john_doe.first_name = "John";
                john_doe.last_name = "Doe";
                _db.Users.Add(john_doe);

                _db.SaveChanges();
            }
        }
    }

    public void InitializeStocks()
    {
        using (var serviceScope = _scopeFactory.CreateScope())
        {
            using (var _db = serviceScope.ServiceProvider.GetService<mainDB>())
            {

                tickers.Add("TSLA");
                tickers.Add("AMZN");
                tickers.Add("AAPL");

                foreach (var tick in tickers)
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(URL);
                    string date_to = DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                    string param = parameters_before_sym + tick + parameters_after_sym + date_to;
                    HttpResponseMessage response = client.GetAsync(param).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        

                        var body = response.Content.ReadAsStringAsync().Result;
                        Root api_returns = JsonConvert.DeserializeObject<Root>(body);

                        int total_pages = (int)Math.Ceiling((double)api_returns.pagination.total / 10000);
                        Console.WriteLine("Total pages: " + total_pages);
                        Console.WriteLine("Total entries: " + api_returns.pagination.total);

                        //for each page, get the data and add it to the database
                        for (int i = 0; i < total_pages; i++)
                        {
                            //get the data
                            param = parameters_before_sym + tick + parameters_after_sym + date_to + "&limit=10000&offset=" + i;
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
                                
                                // find the base stock, if it doesn't exist, create it
                                BaseStock base_stock = _db.baseStocks.Where(b => b.ticker == tick).FirstOrDefault();
                                if (base_stock == null)
                                {
                                    base_stock = new BaseStock();
                                    base_stock.ticker = tick;
                                    base_stock.current_price = ex_stock.price;
                                    _db.baseStocks.Add(base_stock);
                                    _db.SaveChanges();

                                }
                                ex_stock.baseStock = base_stock;
                                _db.historicalStocks.Add(ex_stock);
                            }
                            _db.SaveChanges();

                        }
                    }


                    }
                    _db.SaveChanges();
                }
            }

        }
    
    public void InitializePortfolio()
    {
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var _db = serviceScope.ServiceProvider.GetService<mainDB>())
                {
                    //For every timestamp, create a portfolio for every user

                    foreach (var ts in _db.timestamps)
                    {
                        foreach (var user in _db.Users)
                        {
                            Portfolio portfolio = new Portfolio();
                            portfolio.timestamp = ts;
                            portfolio.user = user;
                            portfolio.total_value = user.curr_balance;
                            portfolio.stock_value = 0;
                            portfolio.liquid_value = user.curr_balance;
                            portfolio.HistoricalStocks = null;                            
                            _db.portfolios.Add(portfolio);
                        }
                    }
                    _db.SaveChanges();
                }
            }
        }
    }

    public void FFillHistoricalStocks()
    {
        using (var serviceScope = _scopeFactory.CreateScope())
        {
            using (var _db = serviceScope.ServiceProvider.GetService<mainDB>())
            {
                //For every timestamp, check if every stock has a historicalStock object for that timestamp


                foreach (var ts in _db.timestamps)
                {
                    foreach (var base_stock in _db.baseStocks)
                    {
                        HistoricalStock hs = _db.historicalStocks.Where(h => h.baseStock == base_stock && h.timestamp == ts).FirstOrDefault();
                        if (hs == null)
                        {
                            HistoricalStock historical_stock = new HistoricalStock();
                            historical_stock.baseStock = base_stock;
                            historical_stock.timestamp = ts;
                            HistoricalStock prev_hs = _db.historicalStocks.Where(h => h.baseStock == base_stock && h.timestamp.time < ts.time).OrderByDescending(h => h.timestamp.time).FirstOrDefault();
                            if (prev_hs == null)
                            {
                                historical_stock.price = -1;
                            }
                            else
                            {
                                historical_stock.price = prev_hs.price;
                            }
                            _db.historicalStocks.Add(historical_stock);
                        }
                    }
                    _db.SaveChanges();
                }
                _db.historicalStocks.RemoveRange(_db.historicalStocks.Where(h => h.price == -1));
                _db.SaveChanges();
            }
        }

    }


    public async void SeedData()
    {
        using (var serviceScope = _scopeFactory.CreateScope())
        {
            using (var _db = serviceScope.ServiceProvider.GetService<mainDB>())
            {
                if (!_db.Users.Any())
                {
                    //Seeding the DB by calling the three functions under

                    //Initialize User
                    InitializeUser();

                    //Initialize Timestamps
                    InitializeTimestamps();

                    //Initialize HistoricalStocks and BaseStocks
                    InitializeStocks();

                    //Initialize Portfolio
                    InitializePortfolio();

                    //Console.WriteLine("FFilling stocks");
                    //FFill HistoricalStocks
                    //FFillHistoricalStocks();

                }
            }
        }
    }
}
