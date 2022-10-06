using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using stock_market.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using System.Security.Policy;



public class DbInitializer : IDbInitializer
{
    private readonly IServiceScopeFactory _scopeFactory;
    private const string URL = "https://api.marketstack.com/v1/intraday";
    private const string parameters = "?access_key=cb0917fe9d7a54323143c281fa427aa2&symbols=AAPL&interval=1min&from_date=2022-10-6&to_date=2022-10-6";

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
            using (var context = serviceScope.ServiceProvider.GetService<mainDB>())
            {

                //add test transaction
                if (!context.Users.Any())
                {

                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(URL);

                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                    // List data response.
                    HttpResponseMessage response = client.GetAsync(parameters).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var body = response.Content.ReadAsStringAsync().Result;
                        Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(body);

                        Console.WriteLine(myDeserializedClass.data.Count);
                        Console.WriteLine(myDeserializedClass.data[0].date);
                        Console.WriteLine(myDeserializedClass.data[0].date);
                        Console.WriteLine(myDeserializedClass.data[0].date);

                    }

                    context.SaveChanges();
                }
            }
        }
    }
}
