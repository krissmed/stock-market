using stock_market.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace stock_market.DAL
{
    public interface ITransactionRepository
    {
        Task<bool> SellStock(string ticker, int amount, int userid);
        Task<bool> BuyStock(string ticker, int amount, int userid);
        Task<List<Transaction>> ListAll(int userid);
    }
}
