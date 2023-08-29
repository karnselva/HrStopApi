using HrApi.DTOs;
using HrApi.Models;
using HrApi.Calculations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HrApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendenceController : ControllerBase
    {
        private readonly HrApiContext _context;

        public AttendenceController(HrApiContext context)
        {
            _context = context;
        }

        [HttpPost("employees/{id}/time-in")]
        public IActionResult AddPunchIn(int id)
        {

            object response;
            var currentDate= DateTime.Now.Date;
            var attendenceDetail = _context.EmployeesAttendances.Where(x => x.EmployeeId == id && x.TimeIn.Value.Date == currentDate && x.TimeOut==null ).OrderByDescending(x => x.TimeIn).FirstOrDefault();
            if (attendenceDetail != null)
            {
                response = new { Messsage = "employee can't timein continous without timeout , wrong in logic" };
                return StatusCode(StatusCodes.Status403Forbidden, response);
            }
            
                    EmployeesAttendance employeesAttendance = new()
                    {
                        TimeIn = DateTime.Now,

                        EmployeeId = id
                    };

                    _context.EmployeesAttendances.Add(employeesAttendance);
                    _context.SaveChanges();

                    response = new { Message = "Attendence Details is Saved successfully" };
                    return StatusCode(StatusCodes.Status201Created, response);
        

            

        }

        [HttpPut("employees/{id}/time-out")]
        public IActionResult AddPunchOut(int id)
        {
            object response;
            var currentDate = DateTime.Now.Date;
            var attendenceDetail = _context.EmployeesAttendances.Where(x => x.EmployeeId == id ).OrderByDescending(x => x.TimeIn).FirstOrDefault();
            if (attendenceDetail != null)
            {
                if(attendenceDetail.TimeOut == null && attendenceDetail?.TimeIn?.Date == currentDate)
                {
                    attendenceDetail.TimeOut = DateTime.Now;
                    _context.SaveChanges();
                    response = new { Message = "Punch Out recorded successfully" };
                    return StatusCode(StatusCodes.Status201Created, response);
                }
                else
                {
                    response = new { Message = "Employee TimeOut prior to timein , something went wrong in logic" };
                    return StatusCode(StatusCodes.Status403Forbidden, response);
                }

            }
            else
            {
                response = new { Message = "Employee should atleast timein once for timeout, logic error" };
                return StatusCode(StatusCodes.Status404NotFound,  response);
            }
        }

        //[HttpGet("employees/{id}/weekly-attendence")]    
        //public IActionResult GetWeeklyReport(int id)
        //{
        //    try
        //    {
        //        List<DateTime> startAndEndDayOfWeek = Calculate.StartAndEndDayOfWeek();
        //        DateTime startDayOfWeek = startAndEndDayOfWeek[0];
        //        DateTime endDayOfWeek = startAndEndDayOfWeek[1];
        //        Console.WriteLine(startDayOfWeek);
        //        Console.WriteLine(endDayOfWeek);
        //        _context.EmployeesAttendances.Where(x => x.EmployeeId==id )
        //        return Ok();
        //    }
        //    catch(IndexOutOfRangeException ex)
        //    {
        //        throw new IndexOutOfRangeException("startAndEndDayOfWeek is out of range", ex);
        //    }
       
        //}
    }
}
