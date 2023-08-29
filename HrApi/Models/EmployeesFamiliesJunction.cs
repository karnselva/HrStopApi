using System;
using System.Collections.Generic;

namespace HrApi.Models;

public partial class EmployeesFamiliesJunction
{
    public int Id { get; set; }

    public int FamilyId { get; set; }

    public int EmployeeId { get; set; }

    public virtual EmployeesDetailsAdmin Employee { get; set; } = null!;

    public virtual FamiliesDetail Family { get; set; } = null!;
}
