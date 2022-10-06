using System.Collections.Generic;

namespace stock_market.Model
{
    public class User
    {
        public int User_id { get; set; }

        public string first_name { get; set; }

        public string last_name { get; set; }

        public int curr_balance { get; set; }

        public string curr_balance_liquid { get; set; }

        public string curr_balance_stock { get; set; }

        public string transactions { get; set; }

        public string watchlist_items { get; set; }

        public string portfolio { get; set; }

    }
}
