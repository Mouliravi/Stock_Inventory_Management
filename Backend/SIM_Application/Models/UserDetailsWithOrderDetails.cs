namespace SIM_Application.Models
{
    public class UserDetailsWithOrderDetails
    {
        public UserDetail UserDetail { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}
