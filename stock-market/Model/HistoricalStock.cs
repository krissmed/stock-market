using System;
using System.Diagnostics.CodeAnalysis;

namespace stock_market.Model
{
    [ExcludeFromCodeCoverage]
    public class HistoricalStock
    {
        
        public int id { get; set; }
        public BaseStock baseStock { get; set; }
        public double price { get; set; }
        public Timestamp timestamp { get; set; }

    }
}

