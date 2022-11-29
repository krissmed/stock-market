using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace stock_market.Model
{
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public double curr_balance { get; set; }
        public double curr_balance_liquid { get; set; }
        public double curr_balance_stock { get; set; }
        public byte[] password { get; set; }
        public byte[] salt { get; set; }
    }
}

