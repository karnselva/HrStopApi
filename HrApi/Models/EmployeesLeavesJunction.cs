using System;
using System.Collections.Generic;

namespace HrApi.Models;

public partial class EmployeesLeavesJunction
{
    public int Id { get; set; }

    public int LeaveCredited { get; set; }

    public int LeaveTaken { get; set; }

    public int EmployeeId { get; set; }

    public int LeaveId { get; set; }

    public virtual EmployeesDetailsAdmin Employee { get; set; } = null!;

    public virtual Leave Leave { get; set; } = null!;
}
