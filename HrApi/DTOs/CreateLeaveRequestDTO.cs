using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HrApi.DTOs
{
    public class CreateLeaveRequestDTO
    {

        public int EmployeeId { get; set; }

        public int LeaveId { get; set; }

        public DateTime LeaveStart { get; set; }

        public DateTime LeaveEnd { get; set; }

        public string Reason { get; set; } = null!;



      
    }
}
