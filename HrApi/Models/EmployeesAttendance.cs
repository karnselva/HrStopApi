using System;
using System.Collections.Generic;

namespace HrApi.Models;

public partial class EmployeesAttendance
{
    public int AttendanceId { get; set; }

    public DateTime? TimeIn { get; set; }

    public DateTime? TimeOut { get; set; }

    public int EmployeeId { get; set; }

    public virtual EmployeesDetailsAdmin Employee { get; set; } = null!;
}
