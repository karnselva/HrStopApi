using HrStop.api.Validation;
using System.ComponentModel.DataAnnotations;

namespace HrApi.DTOs
{
    public class CreateCredentialDTO
    {
        [NotEmpty(ErrorMessage = "Email Address must not be empty.")]
        [RegularExpression(@"^[a-z]+(\.[a-z])?@actualize\.co\.in$", ErrorMessage = "Email Address must have  employeename.employeeinitial.actualize.co.in")]
        public string Email { get; set; } = null!;

        [NotEmpty(ErrorMessage = "Password must not be empty.")]
        [RegularExpression(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$", ErrorMessage = "Password must contain At least one lower case letter,At least one upper case letter,At least special character,At least one number,At least 8 characters length.")]
        public string Password { get; set; } = null!;

        public int EmployeeId { get; set; }
    }
}
