using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace stock_market.Model
{
    [ExcludeFromCodeCoverage]
    public class WatchlistStock
    {
        public int id { get; set; }
        [RegularExpression(@"[a-zA-Z]{2,6}")]
        public BaseStock stock { get; set; }
        [RegularExpression(@"[0-9]{2,8}")]
        public double target_price { get; set; }
        [RegularExpression(@"[0-9]{2,8}")]
        public int amount { get; set; }
    }
}
