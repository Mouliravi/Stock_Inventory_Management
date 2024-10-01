using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SIM_Application.Models;

public partial class Admin
{
    [RegularExpression("^[a-zA-Z]{3,}$", ErrorMessage = "Name is invalid")]
    public string? Name { get; set; }

    public string? Password { get; set; }
}
