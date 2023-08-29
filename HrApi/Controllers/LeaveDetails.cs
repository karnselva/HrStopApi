using HrApi.DTOs;
using HrApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HrApi.Calculations;
using Microsoft.EntityFrameworkCore;

namespace HrApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveDetails : ControllerBase
    {
        private readonly HrApiContext _context;

        public LeaveDetails(HrApiContext context)
        {
            _context = context;
        }

        [HttpPost("employees/{id}/request-leave")]
        public IActionResult RequestLeave(int id, CreateLeaveRequestDTO leaveRequestDTO)
        {

            object response;
            var employeeLeave = _context.EmployeesLeavesJunctions.Where(x => x.EmployeeId == id && x.LeaveId == leaveRequestDTO.LeaveId).FirstOrDefault();
            if (employeeLeave != null)
            {
                // if leave start date is greater than  leave end date swap it
                if (leaveRequestDTO.LeaveStart.Date > leaveRequestDTO.LeaveEnd.Date)
                {
                    DateTime temp = leaveRequestDTO.LeaveStart;
                    leaveRequestDTO.LeaveStart = leaveRequestDTO.LeaveEnd;
                    leaveRequestDTO.LeaveEnd = temp;
                }
                // check request is for present or upcoming days
                if (Calculate.IsPast(leaveRequestDTO.LeaveStart))
                {
                    response = new { Message = "leave can be only request for present or future date" };
                    return StatusCode(StatusCodes.Status403Forbidden, response);
                }

                //getting list of holiday
                List<Holiday> listOfHolidays = _context.Holidays.Where(x => x.Type == "Mandatory").ToList();

                int leaveAvailable = employeeLeave.LeaveCredited - employeeLeave.LeaveTaken;
                int leavesRequested = Calculate.CountWorkingDays(leaveRequestDTO.LeaveStart, leaveRequestDTO.LeaveEnd, listOfHolidays);

                // if request is on mandatory leave
                if (leavesRequested == 0)
                {
                    response = new { Message = "requested day is holiday" };
                    return StatusCode(StatusCodes.Status204NoContent, response);
                }

                if (leaveAvailable >= leavesRequested)
                {

                    LeaveRequest record = new()
                    {
                        EmployeeId = leaveRequestDTO.EmployeeId,
                        LeaveId = leaveRequestDTO.LeaveId,
                        LeaveStart = leaveRequestDTO.LeaveStart.Date,
                        LeaveEnd = leaveRequestDTO.LeaveEnd.Date,
                        Reason = leaveRequestDTO.Reason,
                        Status = false,
                        AppileOn = DateTime.Now.Date
                    };

                    _context.Add(record);
                    _context.SaveChanges();
                    response = new { Message = "leave request is valid and saved" };
                    return StatusCode(StatusCodes.Status201Created, response);
                }
                else
                {
                    response = new { Message = "leaves request is more than leave available", MaxPossibleLeave = leaveAvailable };
                    return StatusCode(StatusCodes.Status403Forbidden, response);
                }
            }
            else
            {
                response = new { Message = "Employee and leave details is not found" };
                return StatusCode(StatusCodes.Status404NotFound, response);
            }




        }

        [HttpGet("manager/{id}/employees/leave-requests")]
        public IActionResult LeavesRequests(int id)
        {
            object response;
            var manager = _context.EmployeesDetailsAdmins.Where(x => x.ReportManagerId == id).Select(item => item.EmployeeId).ToList();
            var leaverequests = _context.LeaveRequests.Where(x => manager.Any(m => m == x.EmployeeId)).Include(x => x.Employee).Include(x => x.Leave).ToList();;

            if (manager != null)
            {
                response = new { Data = leaverequests };
                return StatusCode(StatusCodes.Status200OK, response);

            }
            response = new { Message = "leave Request not found" };

            return StatusCode(StatusCodes.Status404NotFound, response);
        }

        [HttpPut("manager/change-leave-status")]
        public IActionResult ModifyLeaveStatus(ModifyLeaveStatusDTO leaveStatusDTO)
        {

            object response;
            var leaveRequest = _context.LeaveRequests.Where(x => x.Id == leaveStatusDTO.LeaveRequestId).FirstOrDefault();
            if (leaveRequest != null)
            {
                leaveRequest.ApproveOn = DateTime.Now.Date;
                leaveRequest.Status = leaveStatusDTO.Status;
                if (leaveRequest.Status)
                {
                    var employeeLeave = _context.EmployeesLeavesJunctions.Where(x => x.EmployeeId == leaveRequest.EmployeeId).FirstOrDefault();

                    var listOfHolidays = _context.Holidays.Where(x => x.Type == "Mandatory").ToList();

                    if (employeeLeave != null)
                    {
                        int leavesRequested = Calculate.CountWorkingDays(leaveRequest.LeaveStart, leaveRequest.LeaveEnd, listOfHolidays);

                        employeeLeave.LeaveTaken += leavesRequested;
                    }
                    else
                    {
                        response = new { Message = "employee leave junction is null" };
                        return StatusCode(StatusCodes.Status404NotFound, response);
                    }
                }
                _context.SaveChanges();

                response = new { Message = "Leave Status Modified Successfully" };
                return StatusCode(StatusCodes.Status201Created, response);
            }
            else
            {
                response = new { Message = "Leave Request data is unable to find" };
                return StatusCode(StatusCodes.Status404NotFound, response);
            }



        }

        [HttpGet("employees/{id}/check-leaves-status")]
        public IActionResult CheckLeaveStatus(int id)
        {
            var leaverequests = _context.LeaveRequests.Where(x => x.EmployeeId == id).Include(x => x.Employee).Include(x => x.Leave).ToList();

            if (leaverequests != null)
            {
                return StatusCode(StatusCodes.Status200OK, leaverequests);
            }
            else
            {
                var response = new { Message = "leaveRequest table for this employee is null" };
                return StatusCode(StatusCodes.Status404NotFound, response);
            }

        }
    }
}
