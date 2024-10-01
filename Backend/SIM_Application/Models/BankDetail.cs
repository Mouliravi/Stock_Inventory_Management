using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SIM_Application.Models;

public partial class BankDetail
{
    public int BankDetailsId { get; set; }
    [RegularExpression("^[a-zA-Z\\s]{3,}$", ErrorMessage = "Bank Name is invalid" )]
    public string BankName { get; set; } = null!;
    [RegularExpression("^[A-Z]{4}0\\d{6}$",ErrorMessage ="IFSC Code is invalid")]
    public string Ifsc { get; set; } = null!;
    [RegularExpression("^\\d{11}$",ErrorMessage ="MCIR is invalid")]
    public decimal Mcir { get; set; }
	[RegularExpression("^\\d{11}$", ErrorMessage = "Account Number is invalid")]
	public decimal AccountNumber { get; set; }
	[RegularExpression("^[0-9]{4,7}$", ErrorMessage = "Account Balance is invalid")]
	public decimal AccountBalance { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<UserDetail> UserDetails { get; set; } = new List<UserDetail>();
}
