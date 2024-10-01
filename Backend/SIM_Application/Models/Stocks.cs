namespace SIM_Application.Models
{
    public class Stocks
    {
        public UserStockDetail UserStockDetail { get; set; }
        public BrokerDetail BrokerDetail { get; set; }
        public StockDetail StockDetail { get; set; }
        public StockProvider StockProvider { get; set; }

        public StockExchangeTable StockExchangeTable { get; set; }

    }
}
