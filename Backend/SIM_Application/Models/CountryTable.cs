using System;
using System.Collections.Generic;

namespace SIM_Application.Models;

public partial class CountryTable
{
    public int CountryId { get; set; }

    public string? Country { get; set; }

    public virtual ICollection<UserDetail> UserDetails { get; set; } = new List<UserDetail>();
}
