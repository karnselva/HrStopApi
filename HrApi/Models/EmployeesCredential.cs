using System;
using System.Collections.Generic;

namespace HrApi.Models;

public partial class EmployeesCredential
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int EmployeeId { get; set; }

    public virtual EmployeesDetailsAdmin Employee { get; set; } = null!;
}
