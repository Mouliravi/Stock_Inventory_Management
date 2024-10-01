using System;
using System.Collections.Generic;

namespace SIM_Application.Models;

public partial class RoleTable
{
    public int RoleId { get; set; }

    public string? Role { get; set; }

    public virtual ICollection<UserDetail> UserDetails { get; set; } = new List<UserDetail>();
}
