using System;
using System.Collections.Generic;

namespace stock_market.Model
{
    public class Portfolio
    {
        public int total_value;
        public int stock_value;
        public int liquid_value;
        virtual public List<BaseStock> stocks { get; set; }
        virtual public Timestamp timestamp { get; set; }
    }
}

