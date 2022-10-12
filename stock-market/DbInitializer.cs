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

    public async void SeedData()
    {
        using (var serviceScope = _scopeFactory.CreateScope())
        {
            using (var _db = serviceScope.ServiceProvider.GetService<mainDB>())
            {

                if (!_db.Users.Any())
                {

                    //Create Timestamps


                    //Create User
                    User john_doe = new User();
                    john_doe.curr_balance = 100_000;
                    john_doe.curr_balance_liquid = 100_000;
                    john_doe.curr_balance_stock = 0;
                    john_doe.first_name = "John";
                    john_doe.last_name = "Doe";

                    _db.Users.Add(john_doe);

                    //Add historical stocks (and base stocks)
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

                        Console.WriteLine("trying: " + URL + param);
                        Console.WriteLine("trying: " + URL + param);

                        HttpResponseMessage response = client.GetAsync(param).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var body = response.Content.ReadAsStringAsync().Result;
                            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(body);

                            Console.WriteLine("Limit: " + myDeserializedClass.pagination.limit.ToString());
                            Console.WriteLine("Count: " + myDeserializedClass.pagination.count.ToString());
                            Console.WriteLine("Offset: " + myDeserializedClass.pagination.offset.ToString());
                            Console.WriteLine("Total: " + myDeserializedClass.pagination.total.ToString());

                            foreach (var i in myDeserializedClass.data)
                            {
                                HistoricalStock ex_stock = new HistoricalStock();
                                ex_stock.date = i.date;
                                if (i.last != "null")
                                {
                                    double test_double = Convert.ToDouble(i.last, CultureInfo.InvariantCulture);
                                    ex_stock.price = test_double;
                                }
                                else
                                {
                                    double test_double = i.open;
                                    ex_stock.price = test_double;
                                }
                                ex_stock.stock = i.symbol;
                                _db.historicalStocks.Add(ex_stock);
                            }
                        }

                        _db.SaveChanges();
                    }

                }
            }
        }
    }
}
