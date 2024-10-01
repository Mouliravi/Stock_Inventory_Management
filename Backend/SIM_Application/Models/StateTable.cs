using System;
using System.Collections.Generic;

namespace SIM_Application.Models;

public partial class StateTable
{
    public int StateId { get; set; }

    public string? State { get; set; }

    public virtual ICollection<UserDetail> UserDetails { get; set; } = new List<UserDetail>();
}
