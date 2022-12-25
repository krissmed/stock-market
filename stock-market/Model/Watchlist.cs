using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace stock_market.Model
{
    [ExcludeFromCodeCoverage]
    public class Watchlist
    {
        public int id { get; set; }
        public User user { get; set; }
        public List<WatchlistStock> stocks { get; set; }
    }
}

