namespace TestPlatform.Common.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DateComparisonAttribute : ValidationAttribute
    {
        private const string ERROR_MESSAGE = "End date must be after start date.";

        private readonly string startDatePropertyName;

        public DateComparisonAttribute(string startDatePropertyName)
        {
            this.startDatePropertyName = startDatePropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var endDate = (DateTime)value;

            var startDateProperty = validationContext.ObjectType.GetProperty(this.startDatePropertyName);
            if (startDateProperty == null)
            {
                throw new ArgumentException("Invalid start date property name.");
            }

            var startDate = (DateTime)startDateProperty.GetValue(validationContext.ObjectInstance);

            if (endDate < startDate)
            {
                if (string.IsNullOrWhiteSpace(this.ErrorMessage))
                {
                    this.ErrorMessage = ERROR_MESSAGE;
                }

                return new ValidationResult(this.ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
