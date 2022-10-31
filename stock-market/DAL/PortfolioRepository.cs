using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace stock_market.DAL
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly mainDB _db;

        public PortfolioRepository(mainDB db)
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

