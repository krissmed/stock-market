using stock_market.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace stock_market.DAL
{
    public interface IStockRepository
    {
        Task<bool> AddStock(string ticker);
        Task<List<BaseStock>> GetStocks();
        Task<bool> DeleteStock(string ticker);
    }
}
