using System;
namespace stock_market.Model
{
    public class HistoricalStock
    {
        public int id { get; set; }
        public BaseStock baseStock { get; set; }
        public double price { get; set; }
        public Timestamp timestamp { get; set; }

    }
}

