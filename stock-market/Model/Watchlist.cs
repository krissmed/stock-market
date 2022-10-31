using System;
using System.Collections.Generic;

namespace stock_market.Model
{
    public class Watchlist
    {
        public int id { get; set; }
        public User user { get; set; }
        public List<WatchlistStock> stocks { get; set; }
    }
}

