using Microsoft.EntityFrameworkCore;
using stock_market.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Diagnostics.CodeAnalysis;

namespace stock_market.DAL
{
    [ExcludeFromCodeCoverage]
    public class TransactionRepository : ITransactionRepository
    {
        private readonly mainDB _db;

        public TransactionRepository(mainDB db)
        {
            _db = db;
        }

        public async Task<bool> SellStock(string ticker, int amount, int userid)
        {
            {
                try
                {
                    if (!await _db.baseStocks.AnyAsync(s => s.ticker == ticker))
                    {
                        return false;
                    }

                    BaseStock stock = await _db.baseStocks.FirstAsync(s => s.ticker == ticker);
                    User user = await _db.Users.FindAsync(userid);
                    Timestamp timestamp = await _db.timestamps.OrderByDescending(t => t.time).FirstAsync();

                    //find the portfolio for the user and the timestamp
                    Portfolio portfolio = await _db.portfolios
                        .Include(p => p.stock_counter)
                        .ThenInclude(p => p.historical)
                        .ThenInclude(p => p.baseStock)
                        .FirstAsync(p => p.user == user && p.timestamp == timestamp);

                    //find the stock counter for the stock                        
                    BaseStockCounter stock_counter = portfolio.stock_counter.First(s => s.historical.baseStock == stock);

                    //check if the user has enough stocks to sell
                    if (stock_counter.count < amount)
                    {
                        return false;
                    }

                    //sell the stock

                    stock_counter.count -= amount;
                    portfolio.stock_value -= amount * stock_counter.historical.price;
                    portfolio.liquid_value += amount * stock_counter.historical.price;
                    portfolio.total_value = portfolio.stock_value + portfolio.liquid_value;

                    //update the user's balance

                    user.curr_balance_liquid += amount * stock_counter.historical.price;
                    user.curr_balance_stock -= amount * stock_counter.historical.price;
                    user.curr_balance = user.curr_balance_liquid + user.curr_balance_stock;

                    //Create a new transaction

                    Transaction transaction = new Transaction
                    {
                        ticker = stock.ticker,
                        price = stock_counter.historical.price,
                        user = user,
                        quantity = amount,
                        timestamp = timestamp,
                        type = "SELL"
                    };

                    if (stock_counter.count == 0)
                    {
                        portfolio.stock_counter.Remove(stock_counter);
                    }

                    await _db.transactions.AddAsync(transaction);
                    await _db.SaveChangesAsync();

                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
        }

        public async Task<bool> BuyStock(string ticker, int amount, int userid)
        {
            try
            {
                if (!await _db.baseStocks.AnyAsync(s => s.ticker == ticker))
                {
                    return false;
                }

                var stock = await _db.baseStocks.FirstAsync(s => s.ticker == ticker);
                var user = await _db.Users.FirstAsync(u => u.id == userid);
                var timestamp = await _db.timestamps.OrderByDescending(t => t.time)
                    .FirstAsync();


                if (user.curr_balance_liquid < stock.current_price * amount)
                {
                    return false;
                }

                user.curr_balance_liquid -= (stock.current_price * amount);
                user.curr_balance_stock += (stock.current_price * amount);
                user.curr_balance = user.curr_balance_stock + user.curr_balance_liquid;
                await _db.SaveChangesAsync();

                //find the portfolio for the user and the timestamp
                var portfolio = _db.portfolios
                    .Include(p => p.stock_counter)
                    .ThenInclude(p => p.historical)
                    .ThenInclude(p => p.baseStock)
                    .First(p => p.user == user && p.timestamp == timestamp);

                //find the historical stock for the stock and the timestamp
                var historical_stock = await _db.historicalStocks
                    .Include(h => h.baseStock)
                    .FirstAsync(h => h.baseStock == stock && h.timestamp == timestamp);

                //Console.WriteLine(historical_stock.baseStock.ticker);

                if (portfolio.stock_counter == null)
                {
                    portfolio.stock_counter = new List<BaseStockCounter>();
                }

                if (!portfolio.stock_counter.Any(sc => sc.historical == historical_stock))
                {
                    BaseStockCounter counter_object = new BaseStockCounter();
                    counter_object.historical = historical_stock;
                    counter_object.count = amount;
                    portfolio.stock_counter.Add(counter_object);
                }
                else
                {
                    var counter_object = portfolio.stock_counter.First(sc => sc.historical == historical_stock);
                    counter_object.count += amount;
                }


                double value = 0;
                foreach (var counter in portfolio.stock_counter)
                {
                    Console.WriteLine(counter.historical.price);
                    Console.WriteLine(counter.count);

                    value += counter.historical.price * counter.count;
                }

                portfolio.stock_value = user.curr_balance_stock;
                portfolio.liquid_value = user.curr_balance_liquid;
                portfolio.total_value = portfolio.liquid_value + portfolio.stock_value;

                await _db.SaveChangesAsync();

                Transaction new_transaction = new Transaction
                {
                    ticker = ticker,
                    quantity = amount,
                    price = stock.current_price,
                    timestamp = await _db.timestamps.OrderByDescending(t => t.time).FirstAsync(),
                    type = "BUY",
                    user = user
                };

                await _db.transactions.AddAsync(new_transaction);

                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<List<Transaction>> ListAll(int userid)
        {
            var user = await _db.Users.FirstAsync(u => u.id == userid);
            
            List<Transaction> transactions = await _db.transactions
                .Include(t => t.user)
                .Include(t => t.timestamp)
                .Where(t => t.user == user)
                .ToListAsync();
            return transactions;
        }


    }
}

