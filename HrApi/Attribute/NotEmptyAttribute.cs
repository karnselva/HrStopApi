
using System.ComponentModel.DataAnnotations;

namespace HrStop.api.Validation
{


    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class NotEmptyAttribute : ValidationAttribute
    {
        public NotEmptyAttribute()
        {
            ErrorMessage = "The {0} field must not be empty.";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            if (value is string stringValue)
                return !string.IsNullOrWhiteSpace(stringValue);

            return true; // Other types are considered valid
        }
    }
}
