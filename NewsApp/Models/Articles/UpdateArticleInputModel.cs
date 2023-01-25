using NewsApp.Data.Models;
using NewsApp.Services.Mapping;

namespace NewsApp.Models.Articles
{
    public class UpdateArticleInputModel : ArticlesInputModel, IMapFrom<Article>
    {
    }
}
