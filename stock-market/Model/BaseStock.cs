using System;
namespace stock_market.Model
{
    public class BaseStock
    {
        public int id { get; set; }
        public string ticker { get; set; }
        public string name { get; set; }
        public int current_price { get; set; }
    }
}

