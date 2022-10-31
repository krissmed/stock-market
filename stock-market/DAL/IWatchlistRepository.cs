using System.Threading.Tasks;

namespace stock_market.DAL
{
    public interface IWatchlistRepository
    {
        Task<string> GetFullWatchlist();
        Task<bool> AddStock(string ticker, int amount, double target_price);
        Task<bool> DeleteStock(int id);
        Task<bool> UpdateStock(int id, int amount, double target_price);
    }
}
