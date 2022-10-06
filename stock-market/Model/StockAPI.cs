using System;
using System.Collections.Generic;

namespace stock_market.Model
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Datum
    {
        public double open { get; set; }
        public double high { get; set; }
        public double low { get; set; }
        public double last { get; set; }
        public double close { get; set; }
        public double volume { get; set; }
        public DateTime date { get; set; }
        public string symbol { get; set; }
        public string exchange { get; set; }
    }

    public class Pagination
    {
        public int limit { get; set; }
        public int offset { get; set; }
        public int count { get; set; }
        public int total { get; set; }
    }

    public class Root
    {
        public Pagination pagination { get; set; }
        public List<Datum> data { get; set; }
    }

}

