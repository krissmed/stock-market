using System;
using System.Threading.Tasks;

public interface IDbInitializer
{
    Task Initialize();
    Task SeedData();
    Task InitializeTimestamps();
    Task InitializeUser();
    Task InitializeStocks();
    Task InitializeWatchlist();
    Task InitializePortfolio();
    Task FillHistoricalStocks();
    Task FFillHistoricalStocks();
    Task FFillHistoricalStocks(DateTime from);
    Task FillTimestamps(DateTime latest_db, DateTime now);
    Task FillPortfolio(DateTime latest_timestamp);
    Task CheckWatchlists();
    Task UpdateStocks();
    
}
