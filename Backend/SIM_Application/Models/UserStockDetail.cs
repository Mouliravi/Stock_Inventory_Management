using System;
using System.Collections.Generic;

namespace SIM_Application.Models;

public partial class UserStockDetail
{
    public int UserStockId { get; set; }

    public int? UserId { get; set; }

    public int? StockId { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual StockDetail? Stock { get; set; }

    public virtual UserDetail? User { get; set; }
}
