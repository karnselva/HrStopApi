using HrStop.api.Validation;

namespace HrApi.DTOs
{
    public class CreateEmployeeDetailsAdminDTO
    {
        //public int EmployeeId { get; set; } 
        [NotEmpty(ErrorMessage = "Username must not be empty.")]
        public string EmployeeName { get; set; } = null!;

        [NotEmpty(ErrorMessage = "Code must not be empty.")]
        public string Code { get; set; } = null!;
        [NotEmpty(ErrorMessage = "ServiceStatus must not be empty.")]
        public string ServiceStatus { get; set; } = null!;
        [NotEmpty(ErrorMessage = "Role must not be empty.")]

        public string Role { get; set; } = null!;

        [NotEmpty(ErrorMessage = "DateOfJoin must not be empty.")]

        public DateTime DateOfJoin { get; set; }

        public int? ReportManagerId { get; set; }

        public bool IsAdmin { get; set; }

    }
}
