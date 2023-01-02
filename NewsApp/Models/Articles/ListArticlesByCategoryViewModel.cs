using NewsApp.Data.Models;
using NewsApp.Services.Mapping;

namespace NewsApp.Models.Articles
{
    public class ListArticlesByCategoryViewModel : IMapFrom<Article>
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
    }
}
