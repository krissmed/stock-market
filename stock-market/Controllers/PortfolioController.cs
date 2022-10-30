using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using stock_market.Model;

namespace stock_market.Controllers
{
    [Route("[controller]/[action]")]
    public class PortfolioController : ControllerBase
    {
        private readonly mainDB _db;

        public PortfolioController(mainDB db)
        {
            _db = db;
        }

        public async Task<string> GetCurrentPortfolio()
        {
            
            var user = await _db.Users.FirstAsync();
            
            var portfolio = await _db.portfolios
                .Include(p => p.stock_counter)
                .ThenInclude(p => p.historical)
                .ThenInclude(p => p.baseStock)
                .Include(p => p.timestamp)
                .OrderByDescending(p => p.timestamp.time)
                .FirstAsync(p => p.user.id == user.id);

            Console.WriteLine(portfolio.stock_counter.Count);

            string json = JsonConvert.SerializeObject(portfolio);
            return json;
        }

        public async Task<string> GetHistoricalPortfolios()
        {
            var user = await _db.Users.FirstAsync();
            var portfolios = await _db.portfolios
                .Where(p => p.user.id == user.id)
                .ToListAsync();

            string json = JsonConvert.SerializeObject(portfolios);
            return json;
        }

    }
    

}