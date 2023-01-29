using NewsApp.ValidationAttributes;

namespace NewsApp.Models.Articles
{
    public class AddArticlesInputModel : ArticlesInputModel
    {
        [ValidateFileExtension]
        public IEnumerable<IFormFile> Images { get; set; }
    }
}
