using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace stock_market.Model
{
    public class Transaction
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
        public string ticker { get; set; }
        public double price { get; set; }
        public User user { get; set; }
        public string type { get; set; }
        [RegularExpression(@"^[0-9]{4}$")]
        public int quantity { get; set; }
        public Timestamp timestamp { get; set; }
    }
}

