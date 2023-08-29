using System;
using System.Collections.Generic;

namespace HrApi.Models;

public partial class EmployeesDetailsAdmin
{
    public int EmployeeId { get; set; }

    public string EmployeeName { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string ServiceStatus { get; set; } = null!;

    public string Role { get; set; } = null!;

    public DateTime DateOfJoin { get; set; }

    public int? ReportManagerId { get; set; }

    public bool IsAdmin { get; set; }

    [JsonIgnore]
    public virtual ICollection<EmployeeDetail> EmployeeDetails { get; set; } = new List<EmployeeDetail>();
    [JsonIgnore]
    public virtual ICollection<EmployeesAttendance> EmployeesAttendances { get; set; } = new List<EmployeesAttendance>();
    [JsonIgnore]
    public virtual ICollection<EmployeesCredential> EmployeesCredentials { get; set; } = new List<EmployeesCredential>();
    [JsonIgnore]
    public virtual ICollection<EmployeesFamiliesJunction> EmployeesFamiliesJunctions { get; set; } = new List<EmployeesFamiliesJunction>();
    [JsonIgnore]
    public virtual ICollection<EmployeesLeavesJunction> EmployeesLeavesJunctions { get; set; } = new List<EmployeesLeavesJunction>();
    [JsonIgnore]
    public virtual ICollection<EmployeesDetailsAdmin> InverseReportManager { get; set; } = new List<EmployeesDetailsAdmin>();
    [JsonIgnore]
    public virtual ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();
    [JsonIgnore]
    public virtual EmployeesDetailsAdmin? ReportManager { get; set; }
}
