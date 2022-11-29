using System.Diagnostics.CodeAnalysis;

namespace stock_market.Model
{
    [ExcludeFromCodeCoverage]
    public class BaseStockCounter
    {
        public int id { get; set; }
        public HistoricalStock historical { get; set; }
        public int count { get; set; }
    }
}
