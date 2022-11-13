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
        [RegularExpression(@"^[a-zA-ZæøåÆØÅ. \-]{2,20}$")]
        public string first_name { get; set; }
        [RegularExpression(@"^[a-zA-ZæøåÆØÅ. \-]{2,20}$")]
        public string last_name { get; set; }
        [RegularExpression(@"^[0-9]{2,20}$")]
        public double curr_balance { get; set; }
        [RegularExpression(@"^[0-9]{2,20}$")]
        public double curr_balance_liquid { get; set; }
        [RegularExpression(@"^[0-9]{2,20}$")]
        public double curr_balance_stock { get; set; }
    }
}

