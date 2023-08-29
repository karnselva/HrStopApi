using HrApi.Calculations;
using HrApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HrApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyUpdate : ControllerBase
    {
        // next holiday , increament earned sick leave , next payroll
        private readonly HrApiContext _context;

        public DailyUpdate(HrApiContext context)
        {
            _context = context;
        }

        [HttpGet("next-day")]
        public IActionResult NextDayIn()
        {

            object response;
            //throw new Exception("custom");
            // holidays
            List<Holiday> listOfHolidays = _context.Holidays.Where(x => x.Type == "Mandatory").ToList();
            int nextHolidayIn = Calculate.NextHolidayIn(listOfHolidays);
            var Holidaymessage = (nextHolidayIn == -1) ? "Next Holiday will be in Next Calender Year" : $"{nextHolidayIn}";

            //next payroll
            int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);


            response = new { NextHolidayIn = Holidaymessage, NextPayRollIn = daysInMonth - DateTime.Now.Day };
            return StatusCode(StatusCodes.Status200OK, response);



        }

        //[HttpPut("add-leave")]
        //public IActionResult AddLeaves()
        //{
        //    //DateTime today = DateTime.Now.Date;
        //    ////ADD LEAVE
        //    //Type IS SICK LEAVE AND CASUAL LEAVE

        //    //IF LEAVE<12 AND
        //    //IF 1:1:THISYEAR -


        //    //var sickAndEarnedLeave = _context.Leaves.Where(x => x.Name == "CasualLeave" || x.Name == "EarnedLeave");
        //    //Scaffold-DbContext -Connection Name=BookStoresDB Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -force

        //    return Ok();
        //}

    }
}
