using NewsApp.Services.Categories;
using System.ComponentModel.DataAnnotations;

namespace NewsApp.ValidationAttributes
{
    public class DoesCategoryExistInDb : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var catService = validationContext.GetRequiredService<ICategoriesService>();
            if (value == null)
            {
                return new ValidationResult("The category does not exist!");
            }

            bool result = catService.ExistsByIdAsync(value.ToString()).GetAwaiter().GetResult();

            if (result)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("The category does not exist!");
        }
    }
}
