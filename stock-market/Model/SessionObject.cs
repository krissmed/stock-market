using System.Diagnostics.CodeAnalysis;

namespace stock_market.Model
{
    [ExcludeFromCodeCoverage]
    public class SessionObject
    {
        public int userId { get; set; }
        public bool loggedIn { get; set; }
    }
}
