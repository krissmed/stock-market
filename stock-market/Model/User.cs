﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace stock_market.Model
{
    [ExcludeFromCodeCoverage]
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        [RegularExpression(@"[a-zA-Z]{2,15}")]
        public string first_name { get; set; }
        [RegularExpression(@"[a-zA-Z]{2,15}")]
        public string last_name { get; set; }
        [RegularExpression(@"[0-9]{2,15}")]
        public double curr_balance { get; set; }
        [RegularExpression(@"[0-9]{2,15}")]
        public double curr_balance_liquid { get; set; }
        [RegularExpression(@"[0-9]{2,15}")]
        public double curr_balance_stock { get; set; }
        public byte[] password { get; set; }
        public byte[] salt { get; set; }
    }
}

