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
using stock_market.DAL;

namespace stock_market.Controllers
{
    [Route("[controller]/[action]")]
    public class WatchlistController : ControllerBase
    {
        private readonly IWatchlistRepository _db;

        public WatchlistController(IWatchlistRepository db)
        {
            _db = db;
        }

        public async Task<string> GetFullWatchlist()
        {
            return await _db.GetFullWatchlist();
        }

        public async Task<bool> AddStock(string ticker, int amount, double target_price)
        {
            return await _db.AddStock(ticker, amount, target_price);
        }

        public async Task<bool> DeleteStock(int id)
        {
            return await _db.DeleteStock(id);
        }

        public async Task<bool> UpdateStock(int id, int amount, double target_price)
        {
            return await _db.UpdateStock(id, amount, target_price);
        }
    }
}