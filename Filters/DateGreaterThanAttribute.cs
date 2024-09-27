using System.ComponentModel.DataAnnotations;

namespace MediMitra.Filters
{
    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _startDatePropertyName;

        public DateGreaterThanAttribute(string startDatePropertyName)
        {
            _startDatePropertyName = startDatePropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Retrieve the StartDate property from the object
            var startDateProperty = validationContext.ObjectType.GetProperty(_startDatePropertyName);
            if (startDateProperty == null)
            {
                return new ValidationResult($"Unknown property: {_startDatePropertyName}");
            }

            // Get the value of StartDate
            var startDateValue = (DateTime)startDateProperty.GetValue(validationContext.ObjectInstance);

            // Check if EndDate is greater than StartDate
            if (value is DateTime endDateValue && endDateValue > startDateValue)
            {
                return ValidationResult.Success;
            }

            // Return error if validation fails
            return new ValidationResult(ErrorMessage ?? "End Date must be greater than Start Date.");
        }
    }
}
