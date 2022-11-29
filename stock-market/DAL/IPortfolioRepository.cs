using System.Threading.Tasks;

namespace stock_market.DAL
{
    public interface IPortfolioRepository
    {
        Task<string> GetCurrentPortfolio(int userid);
        Task<string> GetHistoricalPortfolios(int userid);

    }
}
