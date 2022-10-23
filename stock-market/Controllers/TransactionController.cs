using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                var timestamp = _db.timestamps.OrderByDescending(t => t.unix).First();


                if (user.curr_balance_liquid < stock.current_price)
                {
                    return false;
                }

                user.curr_balance_liquid -= (stock.current_price*amount);
                user.curr_balance_stock += (stock.current_price*amount);
                user.curr_balance = user.curr_balance_stock + user.curr_balance_liquid;
                _db.SaveChanges();

                //find the portfolio for the user and the timestamp
                var portfolio = _db.portfolios.First(p => p.user.id == user.id && p.timestamp.unix == timestamp.unix);

                //find the basestock
                var baseStock = _db.baseStocks.First(s => s.ticker == ticker);

                //find the historicalstock with the highest timestamp id
                var historicalStock = _db.historicalStocks.OrderByDescending(h => h.timestamp.unix).First(h => h.baseStock.ticker == ticker);

                for (int i = 0; i < amount; i++)
                {
                    portfolio.HistoricalStocks.Add(historicalStock);
                }

                portfolio.total_value = user.curr_balance;
                portfolio.liquid_value = user.curr_balance_liquid;
                portfolio.stock_value = user.curr_balance_stock;
                _db.SaveChanges();

                _db.transactions.Add(new Transaction
                {
                    ticker = ticker,
                    quantity = amount,
                    price = stock.current_price,
                    timestamp = timestamp,
                    type = "BUY"
                });
                _db.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool CreateTransaction(string ticker, int price, int userid)
        {
            try
            {

                Transaction transaction = new Transaction();
                transaction.ticker = ticker;
                transaction.price = price;

                User user = _db.Users.Find(userid);
                RegisterTransaction(transaction, user);

                _db.transactions.Add(transaction);
                _db.SaveChanges();
                return true;

            }
            catch
            {
                return false;
            }

        }

        private static void RegisterTransaction(Transaction transaction, User user)
        {
            user.transactions.Add(transaction);
        }

        public List<Transaction> ListAll()
        {
            List<Transaction> transactions = _db.transactions.ToList();
            return transactions;
        }


    }

}