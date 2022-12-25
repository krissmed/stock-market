using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace stock_market.Model
{
    [ExcludeFromCodeCoverage]
    public class Portfolio
    {
        public int id { get; set; }
        public User user { get; set; }
        public double total_value { get; set; }
        public double stock_value { get; set; }
        public double liquid_value { get; set; }
        public List<BaseStockCounter> stock_counter { get; set; }
        public Timestamp timestamp { get; set; }
    }
}

