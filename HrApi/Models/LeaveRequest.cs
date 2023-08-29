using System;
using System.Collections.Generic;

namespace HrApi.Models;

public partial class LeaveRequest
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public int LeaveId { get; set; }

    public DateTime AppileOn { get; set; }

    public DateTime LeaveStart { get; set; }

    public DateTime LeaveEnd { get; set; }

    public string Reason { get; set; } = null!;

    public DateTime? ApproveOn { get; set; }

    public bool Status { get; set; }

    public virtual EmployeesDetailsAdmin Employee { get; set; } = null!;

    public virtual Leave Leave { get; set; } = null!;
}
