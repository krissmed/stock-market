using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace stock_market.Model
{
    public class Portfolio
    {
        public int id { get; set; }
        public User user { get; set; }
        [RegularExpression(@"^[0-9]{2,20}$")]
        public double total_value { get; set; }
        [RegularExpression(@"^[0-9]{2,20}$")]
        public double stock_value { get; set; }
        [RegularExpression(@"^[0-9]{2,20}$")]
        public double liquid_value { get; set; }
        public List<BaseStockCounter> stock_counter { get; set; }
        public Timestamp timestamp { get; set; }
    }
}

