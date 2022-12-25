using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace stock_market.Model
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    [ExcludeFromCodeCoverage]
    public class Datum
    {
        public double open { get; set; }
        public double high { get; set; }
        public double low { get; set; }
        public string last { get; set; }
        public string close { get; set; }
        public string volume { get; set; }
        public DateTime date { get; set; }
        public string symbol { get; set; }
        public string exchange { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class Pagination
    {
        public int limit { get; set; }
        public int offset { get; set; }
        public int count { get; set; }
        public int total { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class Root
    {
        public Pagination pagination { get; set; }
        public List<Datum> data { get; set; }
    }

}

