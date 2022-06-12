using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Business.Attributes
{
    public class GreaterThanZeroDecimal : ValidationAttribute, IClientModelValidator
    {
        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-positive-decimal", GetErrorMessageOrDefault());
        }

        private string GetErrorMessageOrDefault()
        {
            return ErrorMessage ?? "The number must be positive";
        }

        public override bool IsValid(object? value)
        {
            if (value == null)
                return true;
            var x = (decimal)value;
            return x > 0m;
        }
    }
}
