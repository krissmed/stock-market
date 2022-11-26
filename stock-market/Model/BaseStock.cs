using System;
using System.ComponentModel.DataAnnotations;

namespace stock_market.Model
{
    public class BaseStock
    {
        public int id { get; set; }
        [RegularExpression(@"[a-zA-Z]{2,6}")]
        public string ticker { get; set; }
        public string name { get; set; }
        public double current_price { get; set; }
    }
}

