using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using stock_market.Controllers;
using stock_market.DAL;
using stock_market.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using System.Security.Policy;
using System.Threading.Tasks;

[ExcludeFromCodeCoverage]
public class DbInitializer : IDbInitializer
{
    private readonly IServiceScopeFactory _scopeFactory;

    private List<string> tickers = new List<string>();



    public DbInitializer(IServiceScopeFactory scopeFactory)
    {
        this._scopeFactory = scopeFactory;
    }

    public async Task Initialize()
    {
        using (var serviceScope = _scopeFactory.CreateScope())
        {
            using (var context = serviceScope.ServiceProvider.GetService<mainDB>())
            {
                await context.Database.MigrateAsync();
            }
        }
    }
    

    public async Task InitializeTimestamps()
    {
        using (var serviceScope = _scopeFactory.CreateScope())
        {
            using (var _db = serviceScope.ServiceProvider.GetService<mainDB>())
            {
                string date_from = DateTime.Now.AddDays(-14).ToString("yyyy-MM-dd");
                string URL = "https://api.marketstack.com/v1/intraday";
                string parameters_before_sym = "?access_key=cb0917fe9d7a54323143c281fa427aa2&symbols=";
                string parameters_after_sym = "&interval=1min&date_from=" + date_from + "&date_to=";

                // Create a Timestamp object for every minute since date_from
                DateTime date = DateTime.ParseExact(date_from, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                DateTime now = DateTime.Now;
                List<Timestamp> all_timestamps = new List<Timestamp>();
                while (date < now)
                {
                    date = date.AddMinutes(1);
                    Timestamp timestamp = new Timestamp();
                    timestamp.time = date;
                    timestamp.unix = (int)date.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                    all_timestamps.Add(timestamp);
                }
                await _db.timestamps.AddRangeAsync(all_timestamps);
                await _db.SaveChangesAsync();
                Console.WriteLine("Timestamps added");
            }
        }
    }

    public async Task InitializeUser()
    {
        using (var serviceScope = _scopeFactory.CreateScope())
        {
            using (var _db = serviceScope.ServiceProvider.GetService<mainDB>())
            {
                //Create test users

                User erling = new User();
                erling.username = "ErlingStaff";
                string password = "Erling1!Pass";
                byte[] salt = UserRepository.MakeSalt();
                byte[] hash = UserRepository.MakeHash(password, salt);
                erling.password = hash;
                erling.salt = salt;
                erling.curr_balance = 500_000;
                erling.curr_balance_stock = 0;
                erling.curr_balance_liquid = 500_000;
                erling.first_name = "Erling";
                erling.last_name = "Staff";
                await _db.Users.AddAsync(erling);
                await _db.SaveChangesAsync();


                User ole = new User();
                ole.username = "OleKnoph";
                string passwordOle = "Ole1!Pass";
                byte[] saltOle = UserRepository.MakeSalt();
                byte[] hashOle = UserRepository.MakeHash(passwordOle, saltOle);
                ole.password = hashOle;
                ole.salt = saltOle;
                ole.curr_balance = 250_000;
                ole.curr_balance_stock = 0;
                ole.curr_balance_liquid = 250_000;
                ole.first_name = "Ole";
                ole.last_name = "Knoph";
                await _db.Users.AddAsync(ole);
                await _db.SaveChangesAsync();
                
                User vetle = new User();
                vetle.username = "VetleEndrerud";
                string passwordVetle = "Vetle1!Pass";
                byte[] saltVetle = UserRepository.MakeSalt();
                byte[] hashVetle = UserRepository.MakeHash(passwordVetle, saltVetle);
                vetle.password = hashVetle;
                vetle.salt = saltVetle;
                vetle.curr_balance = 50_000;
                vetle.curr_balance_stock = 0;
                vetle.curr_balance_liquid = 50_000;
                vetle.first_name = "Vetle";
                vetle.last_name = "Endrerud";
                await _db.Users.AddAsync(vetle);
                await _db.SaveChangesAsync();
                
                User kristian = new User();
                kristian.username = "KristianSmedsrød";
                string passwordKristian = "Kristian1!Pass";
                byte[] saltKristian = UserRepository.MakeSalt();
                byte[] hashKristian = UserRepository.MakeHash(passwordKristian, saltKristian);
                kristian.password = hashKristian;
                kristian.salt = saltKristian;
                kristian.curr_balance = 1_000_000;
                kristian.curr_balance_stock = 0;
                kristian.curr_balance_liquid = 1_000_000;
                kristian.first_name = "Kristian";
                kristian.last_name = "Smedsrød";
                await _db.Users.AddAsync(kristian);
                await _db.SaveChangesAsync();
            }
        }
    }
    
    public async Task InitializeStocks()
    {
        using (var serviceScope = _scopeFactory.CreateScope())
        {
            using (var _db = serviceScope.ServiceProvider.GetService<mainDB>())
            {
                string date_from = DateTime.Now.AddDays(-14).ToString("yyyy-MM-dd");
                string URL = "https://api.marketstack.com/v1/intraday";
                string parameters_before_sym = "?access_key=cb0917fe9d7a54323143c281fa427aa2&symbols=";
                string parameters_after_sym = "&interval=1min&date_from=" + date_from + "&date_to=";


                tickers.Add("TSLA");
                tickers.Add("AMZN");
                tickers.Add("AAPL");

                List<HistoricalStock> all_historical_stocks = new List<HistoricalStock>();
                List<Timestamp> all_timestamps = _db.timestamps.ToList();
                List<BaseStock> all_base_stocks = new List<BaseStock>();

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
                                
                                ex_stock.price = Math.Round(ex_stock.price, 3);
                                Timestamp ts = all_timestamps.Find(t => t.time == j.date);
                                ex_stock.timestamp = ts;

                                // find the base stock, if it doesn't exist, create it
                                BaseStock base_stock = all_base_stocks.Find(b => b.ticker == tick);
                                if (base_stock == null)
                                {
                                    base_stock = new BaseStock
                                    {
                                        ticker = tick,
                                        current_price = ex_stock.price
                                    };
                                    all_base_stocks.Add(base_stock);
                                }
                                ex_stock.baseStock = base_stock;
                                all_historical_stocks.Add(ex_stock);
                            }
                        }
                    }
                }

                foreach (var b in all_base_stocks)
                {
                    b.current_price = all_historical_stocks.FindAll(h => h.baseStock == b).First().price;
                }

                await _db.baseStocks.AddRangeAsync(all_base_stocks);
                await _db.historicalStocks.AddRangeAsync(all_historical_stocks);
                await _db.SaveChangesAsync();
            }
        }

    }
    public async Task InitializeWatchlist()
    {
        using (var serviceScope = _scopeFactory.CreateScope())
        {
            using (var _db = serviceScope.ServiceProvider.GetService<mainDB>())
            {
                List<Watchlist> all_watchlists = new List<Watchlist>();
                Console.WriteLine("Filling watchlists");
                List<User> all_users = await _db.Users.ToListAsync();
                foreach (var u in all_users)
                {
                    Watchlist watchlist = new Watchlist();
                    watchlist.user = u;
                    all_watchlists.Add(watchlist);
                }
                await _db.watchlist.AddRangeAsync(all_watchlists);
                await _db.SaveChangesAsync();
            }
        }
    }

    
    public async Task InitializePortfolio()
    {
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var _db = serviceScope.ServiceProvider.GetService<mainDB>())
                {
                    Console.WriteLine("Filling portfolios");

                    //For every timestamp, create a portfolio for every user
                    List<Portfolio> all_portfolios = new List<Portfolio>();
                    foreach (var ts in _db.timestamps)
                    {
                        foreach (var user in _db.Users)
                        {
                            Portfolio portfolio = new Portfolio
                            {
                                timestamp = ts,
                                user = user,
                                total_value = user.curr_balance,
                                stock_value = 0,
                                liquid_value = user.curr_balance,
                                stock_counter = new List<BaseStockCounter>()
                            };
                            all_portfolios.Add(portfolio);
                        }
                    }
                    await _db.portfolios.AddRangeAsync(all_portfolios);
                    await _db.SaveChangesAsync();
                }
            }
        }
    }

    
    public async Task FillHistoricalStocks()
    {
        using (var serviceScope = _scopeFactory.CreateScope())
        {
            using (var _db = serviceScope.ServiceProvider.GetService<mainDB>())
            {

                List<HistoricalStock> new_historical_stocks = new List<HistoricalStock>();
                List<HistoricalStock> replaced_historical_stocks = new List<HistoricalStock>();
                List<HistoricalStock> all_historical_stocks = await _db.historicalStocks
                    .Include(h => h.baseStock)
                    .ToListAsync();
                List<BaseStock> all_base_stocks = await _db.baseStocks.ToListAsync();
                List<Timestamp> all_timestamps = await _db.timestamps.ToListAsync();
                
                Timestamp latest_timestamp = await _db.timestamps.OrderBy(t => t.time).LastAsync();
                
                latest_timestamp.time = latest_timestamp.time.AddDays(-1);

                if (latest_timestamp.time.DayOfWeek == DayOfWeek.Sunday)
                {
                    latest_timestamp.time = latest_timestamp.time.AddDays(-1);
                }


                string date_from = latest_timestamp.time.ToString("yyyy-MM-dd");
                string URL = "https://api.marketstack.com/v1/intraday";
                string parameters_before_sym = "?access_key=cb0917fe9d7a54323143c281fa427aa2&symbols=";
                string parameters_after_sym = "&interval=1min&date_from=" + date_from + "&date_to=";
                string date_to = DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                Console.WriteLine("H");
                foreach (BaseStock base_stock in all_base_stocks) {
                    
                    string ticker = base_stock.ticker;

                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(URL);

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

                                if (all_historical_stocks.Any(h => h.timestamp == ts && h.baseStock == base_stock))
                                {
                                    replaced_historical_stocks.Add(all_historical_stocks.Find(h => h.timestamp == ts && h.baseStock == base_stock));
                                }

                                ex_stock.timestamp = ts;
                                ex_stock.baseStock = base_stock;
                                new_historical_stocks.Add(ex_stock);
                            }
                        }
                    }
                }

                foreach (BaseStock base_stock in all_base_stocks)
                {

                    base_stock.current_price = new_historical_stocks.OrderByDescending(h => h.timestamp.time).First(h => h.baseStock == base_stock).price;
                }

                Console.WriteLine(replaced_historical_stocks.Count);
                _db.historicalStocks.RemoveRange(replaced_historical_stocks);
                await _db.SaveChangesAsync();
                await _db.historicalStocks.AddRangeAsync(new_historical_stocks);
                await _db.SaveChangesAsync();

            }
        }
    }

    public async Task FFillHistoricalStocks()
    {
        using (var serviceScope = _scopeFactory.CreateScope())
        {
            using (var _db = serviceScope.ServiceProvider.GetService<mainDB>())
            {
                //For every timestamp, check if every stock has a historicalStock object for that timestamp

                Console.WriteLine("Filling historical stocks");

                List<HistoricalStock> all_ffilled_historical_stocks = new List<HistoricalStock>();
                List<HistoricalStock> all_historical_stocks = await _db.historicalStocks.ToListAsync();
                List<Timestamp> all_timestamps = await _db.timestamps.ToListAsync();
                List<BaseStock> all_base_stocks = await _db.baseStocks.ToListAsync();
                all_timestamps.Sort((x, y) => x.unix.CompareTo(y.unix));

                foreach (var ts in all_timestamps)
                {
                    foreach (var base_stock in all_base_stocks)
                    {
                        //check if the historical stock exists in the list of historical stocks
                        if (!all_historical_stocks.Any(h => h.timestamp == ts && h.baseStock == base_stock))
                        {
                            HistoricalStock prev_hs = all_historical_stocks.Find(h => h.baseStock == base_stock && h.timestamp.unix == ts.unix - 60);

                            if (prev_hs != null)
                            {
                                HistoricalStock historical_stock = new HistoricalStock();
                                historical_stock.baseStock = base_stock;
                                historical_stock.timestamp = ts;
                                historical_stock.price = prev_hs.price;
                                all_ffilled_historical_stocks.Add(historical_stock);
                                all_historical_stocks.Add(historical_stock);
                            }
                        }
                    }
                }
                await _db.historicalStocks.AddRangeAsync(all_ffilled_historical_stocks);
                await _db.SaveChangesAsync();
            }
        }
    }

    public async Task FFillHistoricalStocks(DateTime from)
    {
        using (var serviceScope = _scopeFactory.CreateScope())
        {
            using (var _db = serviceScope.ServiceProvider.GetService<mainDB>())
            {
                //For every timestamp, check if every stock has a historicalStock object for that timestamp

                //Console.WriteLine("Filling historical stocks from " + from);

                List<HistoricalStock> all_ffilled_historical_stocks = new List<HistoricalStock>();

                List<HistoricalStock> all_historical_stocks = await _db.historicalStocks.ToListAsync();

                List<Timestamp> all_timestamps = await _db.timestamps.Where(t => t.time >= from.AddMinutes(-30)).ToListAsync();
                List<BaseStock> all_base_stocks = await _db.baseStocks.ToListAsync();
                all_timestamps.Sort((x, y) => x.unix.CompareTo(y.unix));

                
                foreach (var ts in all_timestamps)
                {
                    foreach (var base_stock in all_base_stocks)
                    {
                        //check if the historical stock exists in the list of historical stocks
                        if (!all_historical_stocks.Any(h => h.timestamp == ts && h.baseStock == base_stock))
                        {

                            Timestamp time = all_timestamps.Find(t => t.time == ts.time.AddMinutes(-1));
                            //Console.WriteLine("TEST: " + time);
                            //Console.WriteLine("Tried to find: " + time);
                            HistoricalStock prev_hs = all_historical_stocks.Find(h => h.baseStock == base_stock && h.timestamp == time);

                            if (prev_hs != null)
                            {

                                HistoricalStock historical_stock = new HistoricalStock();
                                historical_stock.baseStock = base_stock;
                                historical_stock.timestamp = ts;
                                historical_stock.price = prev_hs.price;
                                all_ffilled_historical_stocks.Add(historical_stock);
                                all_historical_stocks.Add(historical_stock);

                            }
                        }
                    }
                }
                await _db.historicalStocks.AddRangeAsync(all_ffilled_historical_stocks);
                await _db.SaveChangesAsync();
            }
        }
    }

    public async Task FillTimestamps(DateTime latest_db, DateTime now)
    {
        using (var serviceScope = _scopeFactory.CreateScope())
        {
            using (var _db = serviceScope.ServiceProvider.GetService<mainDB>())
            {

                Console.WriteLine("Filling timestamps");
                List<Timestamp> all_timestamps = new List<Timestamp>();

                while (latest_db < now)
                {
                    latest_db = latest_db.AddMinutes(1);
                    Timestamp timestamp = new Timestamp
                    {
                        time = latest_db,
                        unix = (int)latest_db.Subtract(new DateTime(1970, 1, 1)).TotalSeconds
                    };
                    all_timestamps.Add(timestamp);
                }
                await _db.timestamps.AddRangeAsync(all_timestamps);
                await _db.SaveChangesAsync();
                Console.WriteLine("Timestamps added");
            }
        }
    }

    public async Task FillPortfolio(DateTime latest_timestamp)
    {
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var _db = serviceScope.ServiceProvider.GetService<mainDB>())
                {
                    List<Portfolio> all_filled_portfolios = new List<Portfolio>();

                    List<Portfolio> all_portfolios = await _db.portfolios.Include(p => p.stock_counter)
                        .ThenInclude(p => p.historical)
                        .ThenInclude(p => p.baseStock)
                        .Include(p => p.timestamp)
                        .ToListAsync();

                    List<Timestamp> all_timestamps = await _db.timestamps
                        .Where(t => t.time >= latest_timestamp
                        .AddMinutes(-30))
                        .ToListAsync();
                    
                    List<HistoricalStock> all_historical_stocks = await _db.historicalStocks
                        .Include(h => h.baseStock)
                        .Include(h => h.timestamp)
                        .Where(h => h.timestamp.time >= latest_timestamp
                        .AddMinutes(-30))
                        .ToListAsync();
                    
                    List<User> all_users = await _db.Users.ToListAsync();
                    all_timestamps.Sort((x, y) => x.unix.CompareTo(y.unix));


                    foreach (var ts in all_timestamps)
                    {
                        foreach (var user in all_users)
                        {
                            //check if the portfolio exists in the list of portfolio stocks
                            if (!all_portfolios.Any(h => h.timestamp == ts && h.user == user))
                            {
                                
                                Timestamp time = all_timestamps.Find(t => t.time == ts.time.AddMinutes(-1));
                                Portfolio prev_port = all_portfolios.Find(p => p.timestamp == time && p.user == user);
                                
                                double prevval = 0;
                                List<BaseStockCounter> prestock = new List<BaseStockCounter>();
                                
                                if (prev_port == null)
                                {
                                    prevval = user.curr_balance_liquid;
                                }
                                else
                                {
                                    prevval = prev_port.liquid_value;
                                    prestock = prev_port.stock_counter;
                                }

                                Portfolio historical_portfolio = new Portfolio
                                {
                                    user = user,
                                    liquid_value = prevval,
                                    stock_counter = prestock,
                                    stock_value = 0
                                };

                                List<BaseStockCounter> all_counters = new List<BaseStockCounter>();
                                if (historical_portfolio.stock_counter != null && historical_portfolio.stock_counter.Count != 0)
                                {
                                    foreach (var hs in historical_portfolio.stock_counter)
                                    {

                                        BaseStockCounter temp = new BaseStockCounter
                                        {
                                            count = hs.count,
                                        };

                                        HistoricalStock historical_stock = await _db.historicalStocks
                                            .Include(h => h.baseStock)
                                            .Include(h => h.timestamp)
                                            .Where(h => h.baseStock == hs.historical.baseStock && h.timestamp == ts)
                                            .FirstOrDefaultAsync();

                                        temp.historical = historical_stock;

                                        historical_portfolio.stock_value += hs.historical.price * hs.count;
                                        all_counters.Add(temp);
                                    }
                                }


                                historical_portfolio.total_value = historical_portfolio.stock_value
                                    + historical_portfolio.liquid_value;
                                historical_portfolio.timestamp = ts;
                                historical_portfolio.stock_counter = all_counters;
                                all_filled_portfolios.Add(historical_portfolio);
                                all_portfolios.Add(historical_portfolio);
                            }
                        }
                    }

                    //Console.WriteLine(all_filled_portfolios[0].stock_counter.Count);
                    await _db.portfolios.AddRangeAsync(all_filled_portfolios);
                    await _db.SaveChangesAsync();
                }
            }
        }
    }

    
    public async Task CheckWatchlists()
    {
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var _db = serviceScope.ServiceProvider.GetService<mainDB>())
                {

                    List<Watchlist> all_watchlists = await _db.watchlist
                        .Include(w => w.user)
                        .Include(w => w.stocks)
                        .ThenInclude(w => w.stock)
                        .ToListAsync();

                    foreach (var watchlist in all_watchlists)
                    {
                        foreach (var stock in watchlist.stocks)
                        {
                            Console.WriteLine(stock.stock.ticker + " " + stock.stock.current_price);
                            Console.WriteLine(stock.target_price);
                            if (stock.stock.current_price >= stock.target_price)
                            {

                                //buy the stock using the buystock method of the transactioncontroller
                                bool buy = await new TransactionRepository(_db).BuyStock(stock.stock.ticker, stock.amount, watchlist.user.id);

                                if (buy)
                                {
                                    _db.wls.Remove(stock);
                                    await _db.SaveChangesAsync();
                                }
                                else
                                {
                                    Console.WriteLine("Failed to buy");
                                }
                            }
                        }
                    }
                    await _db.SaveChangesAsync();
                }
            }
        }
    }

    public async Task UpdateStocks()
    {
        using (var serviceScope = _scopeFactory.CreateScope())
        {
            using (var _db = serviceScope.ServiceProvider.GetService<mainDB>())
            {
                Console.WriteLine("Updating timestamps");
                //find the latest Timestamp
                Timestamp latest_ts = await _db.timestamps
                    .OrderByDescending(t => t.time)
                    .FirstAsync();
                
                DateTime latest_date = latest_ts.time;

                // If the latest_ts.unix is not now, then fill up untill today with
                DateTime now = DateTime.Now;

                if (latest_date != now)
                {
                    //fill up the timestamps
                    await FillTimestamps(latest_date, now);

                    //Fill stocks
                    await FillHistoricalStocks();

                    //FFill HistoricalStocks
                    await FFillHistoricalStocks(latest_date);
                    
                    //Fill portfolio
                    await FillPortfolio(latest_date);

                    //Check watchlists if any purchases should be fulfilled
                    await CheckWatchlists();

                }
            }
        }
    }

    public async Task SeedData()
    {
        using (var serviceScope = _scopeFactory.CreateScope())
        {
            using (var _db = serviceScope.ServiceProvider.GetService<mainDB>())
            {
                if (!_db.Users.Any())
                {
                    //Seeding the DB by calling the functions under. 
                    // This runs if main.db does not exist, or if it is empty

                    //Initialize User
                    await InitializeUser();

                    //Initialize watchlist
                    await InitializeWatchlist();

                    //Initialize Timestamps
                    await InitializeTimestamps();

                    //Initialize HistoricalStocks and BaseStocks
                    await InitializeStocks();

                    //Initialize Portfolio
                    await InitializePortfolio();

                    //FFill HistoricalStocks
                    await FFillHistoricalStocks();

                }

                //run UpdateStocks asyncronously every minute

                await UpdateStocks();


                //run UpdateStocks syncronously every minute
                
                

                await Task.Run(async () =>
                {
                    while (true)
                    {
                        await Task.Delay(60000);
                        //create a lock around the function updatestocks
                        await UpdateStocks();
                    }
                });

            }
            }
        }
    }
