using System.ComponentModel.DataAnnotations;
using System;

namespace stock_market.Model
{
    public class LoginUser
    {
        [RegularExpression(@"^[a-zA-ZæøåÆØÅ. \-]{2,20}$")]
        public String username { get; set; }
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$")]
        public String password { get; set; }
    }
}
