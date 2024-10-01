using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SIM_Application.Models;

public partial class UserDetail
{
    public int UserId { get; set; }
	[RegularExpression("^[a-zA-Z\\s]{3,}$", ErrorMessage = "User Name is invalid")]
	public string UserName { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }
	public string ValidateDateOfBirth()
	{
		var today = DateOnly.FromDateTime(DateTime.Today);

		if (DateOfBirth > today)
		{
			return "Date of birth cannot be in the future.";
		}

		var eighteenYearsAgo = today.AddYears(-18);
		if (DateOfBirth > eighteenYearsAgo)
		{
			return "User must be at least 18 years old.";
		}

		return null; // Validation passed
	}

	public int? RoleId { get; set; }

    public int? NationalityId { get; set; }

    public int? GenderId { get; set; }

    public int? MaritalStatusId { get; set; }
	[RegularExpression("^[a-zA-Z0-9\\s,]{8,}$", ErrorMessage = "Address Line is invalid")]

	public string AddressLine1 { get; set; } = null!;

    public int? CityId { get; set; }

    public int? StateId { get; set; }

    public int? CountryId { get; set; }
	[RegularExpression("^[789]\\d{9}$", ErrorMessage = "Mobile Number is invalid")]

	public string MobileNumber { get; set; } = null!;
	[RegularExpression("^[a-zA-Z\\s]{3,}$", ErrorMessage = "Occupation is invalid")]
	public string Occupation { get; set; } = null!;
	[RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Email is invalid")]
	public string Email { get; set; } = null!;
	[RegularExpression("^\\d{6,}(?:\\.\\d{1,2})?$", ErrorMessage = "Annual Income is invalid")]
	public decimal AnnualIncome { get; set; }
	[RegularExpression("^\\d{4,}(?:\\.\\d{1,2})?$", ErrorMessage = "Balance account is invalid")]
	public decimal BalanceAmount { get; set; }

    public int? BankDetailsId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual BankDetail? BankDetails { get; set; }

    public virtual CityTable? City { get; set; }

    public virtual CountryTable? Country { get; set; }

    public virtual GenderTable? Gender { get; set; }

    public virtual MaritalStatusTable? MaritalStatus { get; set; }

    public virtual NationalityTable? Nationality { get; set; }

    public virtual RoleTable? Role { get; set; }

    public virtual StateTable? State { get; set; }

    public virtual ICollection<UserStockDetail> UserStockDetails { get; set; } = new List<UserStockDetail>();
}
