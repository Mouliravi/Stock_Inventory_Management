using System;
using System.Collections.Generic;

namespace SIM_Application.Models;

public partial class MaritalStatusTable
{
    public int MaritalStatusId { get; set; }

    public string? MaritalStatus { get; set; }

    public virtual ICollection<UserDetail> UserDetails { get; set; } = new List<UserDetail>();
}
