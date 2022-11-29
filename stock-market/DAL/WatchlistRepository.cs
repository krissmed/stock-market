using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using stock_market.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Diagnostics.CodeAnalysis;

namespace stock_market.DAL
{
    [ExcludeFromCodeCoverage]
    public class WatchlistRepository : IWatchlistRepository
    {
        private readonly mainDB _db;

        public WatchlistRepository(mainDB db)
        {
            _db = db;
        }

        public async Task<string> GetFullWatchlist(int userid)
        {
            User user = await _db.Users.FirstOrDefaultAsync(u => u.id == userid);

            if (!await _db.watchlist.AnyAsync(w => w.user == user))
            {
                Watchlist watch_list = new Watchlist();
                watch_list.user = user;
                watch_list.stocks = new List<WatchlistStock>();
                await _db.watchlist.AddAsync(watch_list);
                await _db.SaveChangesAsync();
            }

            Watchlist watchlist = await _db.watchlist
                .Include(w => w.stocks)
                .ThenInclude(s => s.stock)
                .FirstAsync(w => w.user == user);
            string json = JsonConvert.SerializeObject(watchlist);
            return json;
        }

        public async Task<bool> AddStock(string ticker, int amount, double target_price, int userid)
        {
            try
            {
                if (!await _db.baseStocks.AnyAsync(s => s.ticker == ticker))
                {
                    return false;
                }

                BaseStock stock = await _db.baseStocks.FirstAsync(s => s.ticker == ticker);
                User user = await _db.Users.FirstOrDefaultAsync(u => u.id == userid);

                //check if the user already has a watchlist
                /*if (!_db.watchlist.Any(w => w.user == user))
                {
                    Watchlist new_watchlist = new Watchlist();
                    new_watchlist.user = user;
                    _db.watchlist.Add(new_watchlist);
                    _db.SaveChanges();
                }*/

                Watchlist watchlist = await _db.watchlist
                    .Include(w => w.stocks)
                    .FirstAsync(w => w.user == user);

                WatchlistStock wls = new WatchlistStock
                {
                    stock = stock,
                    amount = amount,
                    target_price = target_price
                };


                watchlist.stocks.Add(wls);
                await _db.wls.AddAsync(wls);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

        }

        public async Task<bool> DeleteStock(int id, int userid)
        {
            try
            {
                WatchlistStock wls = await _db.wls.FirstAsync(w => w.id == id);
                _db.wls.Remove(wls);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<bool> UpdateStock(int id, int amount, double target_price)
        {
            try
            {
                WatchlistStock wls = await _db.wls.FirstAsync(w => w.id == id);
                wls.amount = amount;
                wls.target_price = target_price;
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
}
