using HrApi.DTOs;
using HrApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HrApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Credential : ControllerBase
    {
        private readonly HrApiContext _context;

        public Credential(HrApiContext context)
        {
            _context = context;
        }


        [HttpPost("register")]
        public IActionResult CreateAccount(CreateCredentialDTO newCredential)
        {
            // data validation
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }

            // to send response to user
            object response;

            var employee = _context.EmployeesCredentials.Where(x => x.Email == newCredential.Email).FirstOrDefault();

            if (employee != null)
            {
                response = new { Message = "Email already is Exists , Create new one" };
                return StatusCode(StatusCodes.Status409Conflict, response);
            }
            else
            {

                EmployeesCredential employeesCredential = new()
                {
                    Email = newCredential.Email,
                    Password = newCredential.Password,
                    EmployeeId = newCredential.EmployeeId
                };

                _context.EmployeesCredentials.Add(employeesCredential);
                _context.SaveChanges();

                response = new { Message = "Credential Added Successfully" };
                return StatusCode(StatusCodes.Status201Created, response);
            }


        }

        [HttpPost("login")]
        public IActionResult VerifyAccount(LoginDTO loginCredential)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }

            object response;

            var employee = _context.EmployeesCredentials.Where(x => x.Email == loginCredential.Email).FirstOrDefault();

            if (employee != null)
            {
                if (employee.Password == loginCredential.Password)
                {
                    var employeeDetails = _context.EmployeesDetailsAdmins.Where(x => x.EmployeeId == employee.EmployeeId);

                    response = new { Message = "Account Verified Sucessfully", Data = employeeDetails };

                    return StatusCode(StatusCodes.Status200OK, response);
                }
                else
                {
                    response = new { Message = "Password is Incorrect" };
                    return StatusCode(StatusCodes.Status401Unauthorized, response);
                }

            }
            else
            {
                response = new { Message = "Email Is Not Found, Create New One" };
                return StatusCode(StatusCodes.Status404NotFound, response);
            }


        }

        [HttpPut("employees/{id}/resetpassword")]
        public IActionResult ResetPassword(int id, ResetPasswordDTO resetPasswordDTO)
        {

            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }

            object response;

            var employee = _context.EmployeesCredentials.Where(x => x.EmployeeId == id).FirstOrDefault();
            if (employee != null)
            {
                if (employee.Password == resetPasswordDTO.OrginalPassword)
                {
                    employee.Password = resetPasswordDTO.ResetPassword;
                    _context.SaveChanges();
                    response = new { Message = "Password Reseted Successfully" };
                    return StatusCode(StatusCodes.Status204NoContent, response);
                }
                else
                {
                    response = new { Message = "Orginal Password Is  Incorrect, Try Again" };
                    return StatusCode(StatusCodes.Status401Unauthorized, response);
                }
            }
            else
            {
                response = new { Message = "Email Is Not Found, Create New One" };
                return StatusCode(StatusCodes.Status404NotFound, response);
            }



        }
    }
}
