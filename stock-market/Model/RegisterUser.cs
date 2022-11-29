using System.ComponentModel.DataAnnotations;
using System;

namespace stock_market.Model
{
    public class RegisterUser
    {
        [RegularExpression(@"^[a-zA-ZæøåÆØÅ. \-]{2,20}$")]
        public String username { get; set; }
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,16}$")]
        public String password { get; set; }
        public String first_name { get; set; }
        public String last_name { get; set; }

    }
}