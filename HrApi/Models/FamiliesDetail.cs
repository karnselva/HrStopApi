using System;
using System.Collections.Generic;

namespace HrApi.Models;

public partial class FamiliesDetail
{
    public int FamilyId { get; set; }

    public string Name { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Relationship { get; set; } = null!;

    public string? Address { get; set; }

    public virtual ICollection<EmployeesFamiliesJunction> EmployeesFamiliesJunctions { get; set; } = new List<EmployeesFamiliesJunction>();
}
