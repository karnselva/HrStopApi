using HrStop.api.Validation;
using System.ComponentModel.DataAnnotations;

namespace HrApi.DTOs
{
    public class CreateFamilyDTO
    {
        public string Name { get; set; } = null!;

        [NotEmpty(ErrorMessage = "Gender must not be empty.")]
        public string Gender { get; set; } = null!;

        [NotEmpty(ErrorMessage = "Phonenumber must not be empty.")]
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Phonenumber must be valid.")]
        public string PhoneNumber { get; set; } = null!;

        [NotEmpty(ErrorMessage = "Relationship must not be empty.")]
        public string Relationship { get; set; } = null!;

        public string? Address { get; set; }
    }
}
