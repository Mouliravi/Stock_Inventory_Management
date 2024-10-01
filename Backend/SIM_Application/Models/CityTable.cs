using System;
using System.Collections.Generic;

namespace SIM_Application.Models;

public partial class CityTable
{
    public int CityId { get; set; }

    public string? City { get; set; }

    public virtual ICollection<UserDetail> UserDetails { get; set; } = new List<UserDetail>();
}
