using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using System;
using stock_market.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace stock_market.DAL
{
    [ExcludeFromCodeCoverage]
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly mainDB _db;

        public PortfolioRepository(mainDB db)
        {
            _db = db;
        }

        public async Task<string> GetCurrentPortfolio(int userid)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.id == userid);

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

        public async Task<string> GetHistoricalPortfolios(int userid)
        {
            Console.WriteLine(userid);
            var user = await _db.Users.FirstOrDefaultAsync(u => u.id == userid);
            var portfolios = await _db.portfolios
                .Include(p => p.user)
                .Include(p => p.timestamp)
                .Where(p => p.user == user)
                .Select(p => new
                {
                    p.timestamp.time,
                    p.stock_value,
                    p.liquid_value,
                    p.total_value
                })
                .ToListAsync();
            portfolios.Sort((p1, p2) => p1.time.CompareTo(p2.time));
            
            Console.WriteLine(portfolios.Count);
            
            string json = JsonConvert.SerializeObject(portfolios);
            return json;
        }
    }
}

