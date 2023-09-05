using HrApi.DTOs;
using HrApi.Models;
using HrApi.QueryParameters;
using Microsoft.AspNetCore.Mvc;
using HrApi.Attribute;

namespace HrApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Profile : ControllerBase
    {
        private readonly HrApiContext _context;

        public Profile(HrApiContext context)
        {
            _context = context;
        }

        [HttpPost("admins/{id}/create-employee")]
        [ValidateModel]
        public IActionResult CreateEmployee(int id, CreateEmployeeDetailsAdminDTO employeeDetailsAdminDTO)
        {

            object response;

            var employee = _context.EmployeesDetailsAdmins.Where(x => x.EmployeeId == id).FirstOrDefault();

            if (employee != null)
            {
                if (employee.IsAdmin == true)
                {
                    EmployeesDetailsAdmin employeesDetailsAdmin = new()
                    {

                        EmployeeName = employeeDetailsAdminDTO.EmployeeName,
                        Code = employeeDetailsAdminDTO.Code,
                        DateOfJoin = employeeDetailsAdminDTO.DateOfJoin.Date,
                        ServiceStatus = employeeDetailsAdminDTO.ServiceStatus,
                        ReportManagerId = employeeDetailsAdminDTO.ReportManagerId,
                        Role = employeeDetailsAdminDTO.Role
                    };

                    _context.EmployeesDetailsAdmins.Add(employeesDetailsAdmin);
                    _context.SaveChanges();

                    response = new { Message = "Details Added Successfully", Data = employeesDetailsAdmin };
                    return StatusCode(StatusCodes.Status201Created, response);

                }
                else
                {
                    response = new { Message = "Only Admin can create a employee details" };
                    return StatusCode(StatusCodes.Status403Forbidden, response);
                }
            }
            else
            {
                response = new { admin = "Admin details is not found" };
                return StatusCode(StatusCodes.Status404NotFound, response);
            }


        }

        [HttpGet("employee-admin-details/employees/{id}")]
        [ValidateModel]
        public IActionResult GetEmployeeAdminDetails(int employeeId)
        {
            object response;

            var employeeAdminDetail = _context.EmployeesDetailsAdmins.Where(x => x.EmployeeId == employeeId).FirstOrDefault();
            if (employeeAdminDetail != null)
            {
                response = new { Data = employeeAdminDetail };
                return StatusCode(StatusCodes.Status200OK, response);
            }
            else
            {
                response = new { message = "employee-admin-details not found" };
                return StatusCode(StatusCodes.Status404NotFound, response);
            }

        }


        [HttpGet("/search-employees")]
        [ValidateModel]
        public IActionResult SearchEmployees([FromQuery] SearchEmployeeQueryParameters parameters)
        {
            object response;
            var employees = _context.EmployeesDetailsAdmins.Where(x => x.EmployeeName.Contains(parameters.Name)).ToList();

            response = new { employees };
            return StatusCode(StatusCodes.Status200OK, response);

        }

        [HttpDelete("admins/{adminId}/employees/{employeeId}/delete-employee-account")]
        [ValidateModel]
        public IActionResult DeleteAccount(int adminId, int employeeId)
        {

            object response;


            var admin = _context.EmployeesDetailsAdmins.Where(x => x.EmployeeId == adminId).FirstOrDefault();

            if (admin != null)
            {
                if (admin.IsAdmin == true)
                {
                    var employee = _context.EmployeesDetailsAdmins.Where(x => x.EmployeeId == employeeId).FirstOrDefault();
                    if (employee != null)
                    {

                        _context.EmployeesDetailsAdmins.Remove(employee);
                        _context.SaveChanges();
                        response = new { Message = "Account Deleted Successfully" };
                        return StatusCode(StatusCodes.Status200OK, response);

                    }
                    else
                    {
                        response = new { Message = "Employee Is Not Found" };
                        return StatusCode(StatusCodes.Status404NotFound, response);
                    }

                }

                else
                {
                    response = new { Message = "Only Admin can create a employee details" };
                    return StatusCode(StatusCodes.Status403Forbidden, response);
                }
            }
            else
            {
                response = new { admin = "Admin details is not found" };
                return StatusCode(StatusCodes.Status404NotFound, response);
            }

        }

        [HttpPost("employees/{id}/family-detail")]
        [ValidateModel]
        public IActionResult CreateFamily(int id, CreateFamilyDTO family)
        {
           
            object response;

            FamiliesDetail familiesDetail = new()
            {
                Name = family.Name,
                Gender = family.Gender,
                Relationship = family.Relationship,
                Address = family.Address,
                PhoneNumber = family.PhoneNumber,
            };

            _context.Add(familiesDetail);
            _context.SaveChanges();

            int familyId = familiesDetail.FamilyId;

            EmployeesFamiliesJunction employeesFamiliesJunction = new()
            {
                FamilyId = familyId,
                EmployeeId = id
            };

            _context.Add(employeesFamiliesJunction);
            _context.SaveChanges();

            response = new { Message = "Details Saved Successfully" };
            return StatusCode(StatusCodes.Status201Created, response);



        }
    }
}
