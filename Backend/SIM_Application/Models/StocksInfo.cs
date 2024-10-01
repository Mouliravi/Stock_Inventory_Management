namespace SIM_Application.Models
{
    public class StocksInfo
    {
        public UserStockDetail UserStockDetail { get; set; }
        public StockDetail StockDetail { get; set; }
        public StockProvider StockProvider { get; set; }
        public BrokerDetail BrokerDetail { get; set; }
        public OrderDetail OrderDetail { get; set; }
    }
}
