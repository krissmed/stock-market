using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using stock_market.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace stock_market.DAL
{
    [ExcludeFromCodeCoverage]
    public class HistoricalStockRepository : IHistoricalStockRepository
    {
        private readonly mainDB _db;

        public HistoricalStockRepository(mainDB db)
        {
            _db = db;
        }

        public async Task<string> GetHistoricalPrice(string ticker)
        {

            if (ticker == null)
            {
                var ret = "Please enter a ticker";
                return ret;
            }
            //rewrite the code under to use async await

            BaseStock stock = await _db.baseStocks.FirstAsync(s => s.ticker == ticker);


            //get all entries where in historicalstocks where h.baseStock == stock
            List<HistoricalStock> historicalstock = await _db.historicalStocks
                .Where(h => h.baseStock == stock)
                .Include(h => h.timestamp)
                .ToListAsync();

            //sort the list by timestamp
            historicalstock.Sort((x, y) => x.timestamp.time.CompareTo(y.timestamp.time));
            string json = JsonConvert.SerializeObject(historicalstock);
            return json;
        }
    }
}
