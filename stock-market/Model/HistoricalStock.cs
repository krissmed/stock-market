using System;
namespace stock_market.Model
{
    public class HistoricalStock
    {
        public int id { get; set; }
        virtual public BaseStock baseStock { get; set; }
        public double price { get; set; }
        virtual public Timestamp timestamp { get; set; }

    }
}

