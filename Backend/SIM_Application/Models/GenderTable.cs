using System;
using System.Collections.Generic;

namespace SIM_Application.Models;

public partial class GenderTable
{
    public int GenderId { get; set; }

    public string? Gender { get; set; }

    public virtual ICollection<UserDetail> UserDetails { get; set; } = new List<UserDetail>();
}
