using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using stock_market.Model;
using Microsoft.AspNetCore.Authentication;

namespace stock_market.Controllers
{
    [Route("[controller]/[action]")]
    public class WatchlistController : ControllerBase
    {
        private readonly mainDB _db;

        public WatchlistController(mainDB db)
        {
            _db = db;
        }

        public string GetFullWatchlist()
        {
            User user = _db.Users.First();

            if (!_db.watchlist.Any(w => w.user == user))
            {
                Watchlist watch_list = new Watchlist();
                watch_list.user = user;
                watch_list.stocks = new List<WatchlistStock>();
                _db.watchlist.Add(watch_list);
                _db.SaveChanges();
            }

            Watchlist watchlist = _db.watchlist
                .Include(w => w.stocks)
                .ThenInclude(s => s.stock)
                .First(w => w.user == user);
            string json = JsonConvert.SerializeObject(watchlist);
            return json;
        }

        
        public bool AddStock(string ticker, int amount, double target_price)
        {
            try
            {
                if (!_db.baseStocks.Any(s => s.ticker == ticker))
                {
                    return false;
                }

                BaseStock stock = _db.baseStocks.First(s => s.ticker == ticker);
                User user = _db.Users.First();

                //check if the user already has a watchlist
                /*if (!_db.watchlist.Any(w => w.user == user))
                {
                    Watchlist new_watchlist = new Watchlist();
                    new_watchlist.user = user;
                    _db.watchlist.Add(new_watchlist);
                    _db.SaveChanges();
                }*/

                Watchlist watchlist = _db.watchlist
                    .Include(w => w.stocks)
                    .First(w => w.user == user);

                Console.WriteLine(watchlist.user.first_name);

                WatchlistStock wls = new WatchlistStock
                {
                    stock = stock,
                    amount = amount,
                    target_price = target_price
                };
                
                
                watchlist.stocks.Add(wls);
                _db.wls.Add(wls);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

        }

        public bool DeleteStock(int id)
        {
            {
                try
                {
                    WatchlistStock wls = _db.wls.First(w => w.id == id);
                    _db.wls.Remove(wls);
                    _db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
        }

        public bool UpdateStock(int id, int amount, double target_price)
        {
            try
            {
                WatchlistStock wls = _db.wls.First(w => w.id == id);
                wls.amount = amount;
                wls.target_price = target_price;
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

    }
}