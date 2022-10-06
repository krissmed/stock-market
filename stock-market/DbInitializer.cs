using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using stock_market.Model;
using System;
using System.Linq;

public class DbInitializer : IDbInitializer
{
    private readonly IServiceScopeFactory _scopeFactory;

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

    public void SeedData()
    {
        using (var serviceScope = _scopeFactory.CreateScope())
        {
            using (var context = serviceScope.ServiceProvider.GetService<mainDB>())
            {

                //add test transaction
                if (!context.Users.Any())
                {
                    var test_transaction = new Transaction
                    {
                        id = 1,
                        ticker = "spy",
                        price = 9_999
                    };
                    context.transactions.Add(test_transaction);
                }

                context.SaveChanges();
            }
        }
    }
}
