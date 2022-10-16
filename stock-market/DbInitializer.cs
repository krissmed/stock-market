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

    private const string date_from = "2022-10-05";
    private const string URL = "https://api.marketstack.com/v1/intraday";
    private const string parameters_before_sym = "?access_key=cb0917fe9d7a54323143c281fa427aa2&symbols=";
    private const string parameters_after_sym = "&interval=1min&from_date=" + date_from + "&to_date=";


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
                    Console.WriteLine("USING: " + param);
                    HttpResponseMessage response = client.GetAsync(param).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        

                        var body = response.Content.ReadAsStringAsync().Result;
                        Root api_returns = JsonConvert.DeserializeObject<Root>(body);

                        //find total pages with pagination.total / 100. Use math.ceil to round up
                        int total_pages = (int)Math.Ceiling((double)api_returns.pagination.total / 100);

                        //for each page, get the data and add it to the database
                        for (int i = 0; i < total_pages; i++)
                        {
                            //get the data
                            param = parameters_before_sym + tick + parameters_after_sym + date_to + "&limit=100&offset=" + i;
                            response = client.GetAsync(param).Result;
                            body = response.Content.ReadAsStringAsync().Result;
                            api_returns = JsonConvert.DeserializeObject<Root>(body);
                            
                            foreach (var j in api_returns.data)
                            {
                                HistoricalStock ex_stock = new HistoricalStock();
                                if (j.last != "null")
                                {
                                    double test_double = Convert.ToDouble(j.last, CultureInfo.InvariantCulture);
                                    ex_stock.price = test_double;
                                }
                                else
                                {
                                    double test_double = j.open;
                                    ex_stock.price = test_double;
                                }
                                ex_stock.timestamp = _db.timestamps.Where(t => t.time == j.date).FirstOrDefault();

                                // find the base stock, if it doesn't exist, create it
                                BaseStock base_stock = _db.baseStocks.Where(b => b.ticker == tick).FirstOrDefault();
                                if (base_stock == null)
                                {
                                    base_stock = new BaseStock();
                                    base_stock.ticker = tick;
                                    _db.baseStocks.Add(base_stock);
                                    _db.SaveChanges();
                                }
                                ex_stock.baseStock = base_stock;
                                _db.historicalStocks.Add(ex_stock);
                                _db.SaveChanges();
                            }

                        }
                    }


                    }
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
                    
                    //Initialize Timestamps
                    InitializeTimestamps();

                    //Initialize User
                    InitializeUser();

                    //Initialize HistoricalStocks and BaseStocks
                    InitializeStocks();

                }
            }
        }
    }
}
