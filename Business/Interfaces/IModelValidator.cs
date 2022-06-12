using System.ComponentModel.DataAnnotations;

namespace Business.Interfaces
{
    public interface IModelValidator<TModel> where TModel : class
    {
        bool IsValid(TModel model, out string? errorMessage);

        protected static bool IsValidByDefault(TModel model, out string? errorMessage)
        {
            errorMessage = null;
            if (model is null)
            {
                errorMessage = "Model cannot be null";
                return false;
            }
            var validationContext = new ValidationContext(model);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(model, validationContext, validationResults, true);
            if (!isValid)
            {
                errorMessage = string.Join(Environment.NewLine, validationResults.Select(r => r.ErrorMessage));
                return false;
            }
            return true;
        }
    }
}
