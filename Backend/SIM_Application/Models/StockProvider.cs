using System;
using System.Collections.Generic;

namespace SIM_Application.Models;

public partial class StockProvider
{
    public int ProviderId { get; set; }

    public string ProviderName { get; set; } = null!;

    public decimal PerStockPrice { get; set; }

    public int AvailableStocksQuantity { get; set; }

    public decimal ExpenseRatio { get; set; }

    public int? StockExchangeId { get; set; }

    public DateOnly? CreatedAt { get; set; }

    public DateOnly? UpdatedAt { get; set; }

    public virtual ICollection<StockDetail> StockDetails { get; set; } = new List<StockDetail>();

    public virtual StockExchangeTable? StockExchange { get; set; }
}
