using System;
using System.Collections.Generic;

namespace HrApi.Models;

public partial class Holiday
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public DateTime HolidayDate { get; set; }

    public string Name { get; set; } = null!;
}
