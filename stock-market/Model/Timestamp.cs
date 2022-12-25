using System;
using System.Diagnostics.CodeAnalysis;

namespace stock_market.Model
{
    [ExcludeFromCodeCoverage]
    public class Timestamp
    {
        public int id { get; set; }
        public DateTime time { get; set; }
        public int unix { get; set; }
    }
}

