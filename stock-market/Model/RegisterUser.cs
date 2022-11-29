using System.ComponentModel.DataAnnotations;
using System;

namespace stock_market.Model
{
    public class RegisterUser
    {
        [RegularExpression(@"^[a-zA-ZæøåÆØÅ. \-]{2,20}$")]
        public string username { get; set; }
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,16}$")]
        public string password { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }

    }
}