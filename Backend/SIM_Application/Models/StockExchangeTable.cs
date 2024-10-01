using System;
using System.Collections.Generic;

namespace SIM_Application.Models;

public partial class StockExchangeTable
{
    public int StockExchangeId { get; set; }

    public string? StockExchange { get; set; }

    public virtual ICollection<StockProvider> StockProviders { get; set; } = new List<StockProvider>();
}
