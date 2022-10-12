using System;
namespace stock_market.Model
{
    public class HistoricalStock
    {
        public int id { get; set; }
        public string stock { get; set; }
        public double price { get; set; }
        public DateTime date { get; set; }
    }
}

