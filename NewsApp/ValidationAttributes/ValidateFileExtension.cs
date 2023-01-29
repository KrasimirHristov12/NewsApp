using System.ComponentModel.DataAnnotations;

namespace NewsApp.ValidationAttributes
{
    public class ValidateFileExtension : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var files = value as IEnumerable<IFormFile>;
                var supportedTypes = new[] { "png", "jpg", "jpeg" };
                foreach (var file in files)
                {
                    var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                    if (!supportedTypes.Contains(fileExt))
                    {
                        return new ValidationResult("File extension is invalid");
                    }
                }
 
                return ValidationResult.Success;
            }
            return ValidationResult.Success;


        }
    }
}
