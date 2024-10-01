using System;
using System.Collections.Generic;

namespace SIM_Application.Models;

public partial class StockDetail
{
    public int StockId { get; set; }

    public int? ProviderId { get; set; }

    public int? BrokerId { get; set; }

    public virtual BrokerDetail? Broker { get; set; }

    public virtual StockProvider? Provider { get; set; }

    public virtual ICollection<UserStockDetail> UserStockDetails { get; set; } = new List<UserStockDetail>();
}
