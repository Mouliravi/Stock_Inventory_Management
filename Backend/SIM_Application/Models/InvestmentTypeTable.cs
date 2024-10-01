using System;
using System.Collections.Generic;

namespace SIM_Application.Models;

public partial class InvestmentTypeTable
{
    public int InvestmentTypeId { get; set; }

    public string? InvestmentType { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
