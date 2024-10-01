using System;
using System.Collections.Generic;

namespace SIM_Application.Models;

public partial class NationalityTable
{
    public int NationalityId { get; set; }

    public string? Nationality { get; set; }

    public virtual ICollection<UserDetail> UserDetails { get; set; } = new List<UserDetail>();
}
