using HrApi.Attribute;
using HrApi.DTOs;
using HrApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace HrApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Credential : ControllerBase
    {
        private readonly HrApiContext _context;
        private readonly IMapper _mapper;
        public Credential(HrApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpPost("register")]
        [ValidateModel]
        public IActionResult CreateAccount(CreateCredentialDTO newCredential)
        {
            object response;

            var employee = _context.EmployeesCredentials.Where(x => x.Email == newCredential.Email).FirstOrDefault();

            if (employee != null)
            {
                response = new { Message = "Email already is Exists , Create new one" };
                return StatusCode(StatusCodes.Status409Conflict, response);
            }
            
                EmployeesCredential employeesCredential = _mapper.Map<EmployeesCredential>(newCredential);
                _context.EmployeesCredentials.Add(employeesCredential);
                _context.SaveChanges();

                response = new { Message = "Credential Added Successfully" };
                return StatusCode(StatusCodes.Status201Created, response);
          


        }

        [HttpPost("login")]
        [ValidateModel]
        public IActionResult VerifyAccount(LoginDTO loginCredential)
        {
            

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
        [ValidateModel]
        public IActionResult ResetPassword(int id, ResetPasswordDTO resetPasswordDTO)
        {

            

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
