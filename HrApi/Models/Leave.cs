using System;
using System.Collections.Generic;

namespace HrApi.Models;

public partial class Leave
{
    public int LeaveId { get; set; }

    public int Entitled { get; set; }

    public string Name { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<EmployeesLeavesJunction> EmployeesLeavesJunctions { get; set; } = new List<EmployeesLeavesJunction>();
    [JsonIgnore]
    public virtual ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();
}
