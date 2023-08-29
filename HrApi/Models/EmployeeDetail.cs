using System;
using System.Collections.Generic;

namespace HrApi.Models;

public partial class EmployeeDetail
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string DateOfBirth { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string AadharNumber { get; set; } = null!;

    public string Pan { get; set; } = null!;

    public int EmployeeId { get; set; }

    public virtual EmployeesDetailsAdmin Employee { get; set; } = null!;
}
