using System.Threading.Tasks;

namespace stock_market.DAL
{
    public interface IHistoricalStockRepository
    {
        Task<string> GetHistoricalPrice(string ticker);
    }
}
