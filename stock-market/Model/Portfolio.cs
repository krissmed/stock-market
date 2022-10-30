using System;
using System.Collections.Generic;

namespace stock_market.Model
{
    public class Portfolio
    {
        public int id { get; set; }
        virtual public User user { get; set; }
        public double total_value { get; set; }
        public double stock_value { get; set; }
        public double liquid_value { get; set; }
        public List<HistoricalStock> HistoricalStocks { get; set; }
        public Timestamp timestamp { get; set; }
    }
}

