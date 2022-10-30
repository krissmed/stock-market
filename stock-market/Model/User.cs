using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace stock_market.Model
{
    public class User
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public double curr_balance { get; set; }
        public double curr_balance_liquid { get; set; }
        public double curr_balance_stock { get; set; }
        public List<Transaction> transactions { get; set; }
    }
}

