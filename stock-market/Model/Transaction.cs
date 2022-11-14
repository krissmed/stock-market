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
        [RegularExpression(@"^[a-zA-ZæøåÆØÅ. \-]{2,20}$")]
        public string ticker { get; set; }
        [RegularExpression(@"^[0-9]{2,20}$")]
        public double price { get; set; }
        public User user { get; set; }
        [RegularExpression(@"^[a-zA-ZæøåÆØÅ. \-]{2,20}$")]
        public string type { get; set; }
        [RegularExpression(@"^[0-9]{2,20}$")]
        public int quantity { get; set; }
        public Timestamp timestamp { get; set; }
    }
}

