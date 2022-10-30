using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using stock_market.Model;

namespace stock_market.Controllers
{
    [Route("[controller]/[action]")]
    public class TransactionController : ControllerBase
    {
        private readonly mainDB _db;

        public TransactionController(mainDB db)
        {
            _db = db;
        }

        public bool BuyStock(string ticker, int amount)
        {
            try
            {
                if (!_db.baseStocks.Any(s => s.ticker == ticker))
                {
                    return false;
                }

                var stock = _db.baseStocks.First(s => s.ticker == ticker);
                var user = _db.Users.First();
                var timestamp = _db.timestamps.OrderByDescending(t => t.time).First();


                if (user.curr_balance_liquid < stock.current_price*amount)
                {
                    return false;
                }

                user.curr_balance_liquid -= (stock.current_price*amount);
                user.curr_balance_stock += (stock.current_price*amount);
                user.curr_balance = user.curr_balance_stock + user.curr_balance_liquid;
                _db.SaveChanges();

                //find the portfolio for the user and the timestamp
                var portfolio = _db.portfolios.First(p => p.user == user && p.timestamp == timestamp);

                //find the basestock
                var baseStock = _db.baseStocks.First(s => s.ticker == ticker);

                var historicalStock = _db.historicalStocks.First(h => h.baseStock.ticker == ticker && h.timestamp == timestamp);

                if (portfolio.HistoricalStocks == null)
                {
                    portfolio.HistoricalStocks = new List<HistoricalStock>();
                }
                for (int i = 0; i < amount; i++)
                {
                    portfolio.HistoricalStocks.Add(historicalStock);
                }

                portfolio.total_value = user.curr_balance;
                portfolio.liquid_value = user.curr_balance_liquid;
                portfolio.stock_value = user.curr_balance_stock;
                _db.SaveChanges();

                Transaction new_transaction = new Transaction
                {
                    ticker = ticker,
                    quantity = amount,
                    price = stock.current_price,
                    timestamp = _db.timestamps.OrderByDescending(t => t.time).First(),
                    type = "BUY",
                    user = user
                };
                
                _db.transactions.Add(new_transaction);

                _db.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }



        public List<Transaction> ListAll()
        {
            //include user and timestamp
            List<Transaction> transactions = _db.transactions.Include(t => t.user).Include(t => t.timestamp).ToList();
            return transactions;
        }


    }

}