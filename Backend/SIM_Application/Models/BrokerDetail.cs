using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SIM_Application.Models;

public partial class BrokerDetail
{
    public int BrokerId { get; set; }
    public string BrokerName { get; set; } = null!;

    public decimal CommissionPercentage { get; set; }

    public virtual ICollection<StockDetail> StockDetails { get; set; } = new List<StockDetail>();
}
