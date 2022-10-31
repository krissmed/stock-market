namespace stock_market.Model
{
    public class WatchlistStock
    {
        public int id { get; set; }
        public BaseStock stock { get; set; }
        public double target_price { get; set; }
        public int amount { get; set; }
    }
}
