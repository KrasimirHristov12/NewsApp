using NewsApp.ValidationAttributes;

namespace NewsApp.Models.Articles
{
    public class AddArticlesInputModel : ArticlesInputModel
    {
        [ValidateFileExtension]
        public IFormFile? Image { get; set; }
    }
}
