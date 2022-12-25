using System.Threading.Tasks;

namespace stock_market.DAL
{
    public interface IWatchlistRepository
    {
        Task<string> GetFullWatchlist(int userid);
        Task<bool> AddStock(string ticker, int amount, double target_price, int userid);
        Task<bool> DeleteStock(int id, int userid);
        Task<bool> UpdateStock(int id, int amount, double target_price);
    }
}
