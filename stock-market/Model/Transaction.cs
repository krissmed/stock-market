using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace stock_market.Model
{
    [ExcludeFromCodeCoverage]
    public class Transaction
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
        [RegularExpression(@"[a-zA-Z]{2,6}")]
        public string ticker { get; set; }
        public double price { get; set; }
        public User user { get; set; }
        public string type { get; set; }
        [RegularExpression(@"([1-9]\d{0,3})?")]
        public int quantity { get; set; }
        public Timestamp timestamp { get; set; }
    }
}

