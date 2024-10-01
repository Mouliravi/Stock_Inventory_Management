using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SIM_Application.Models;

public partial class OrderDetail
{
    public int OrderId { get; set; }

    public DateOnly? OrderDate { get; set; }
    [Required]
    public int? PurchasedQuantity { get; set; }

    public int? InvestmentTypeId { get; set; }

    public decimal? TotalInvestment { get; set; }

    public string? BarCode { get; set; }

    public int? UserStockId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual InvestmentTypeTable? InvestmentType { get; set; }

    public virtual UserStockDetail? UserStock { get; set; }
}
