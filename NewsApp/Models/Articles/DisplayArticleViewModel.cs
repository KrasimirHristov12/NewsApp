using NewsApp.Data.Models;
using NewsApp.Services.Mapping;

namespace NewsApp.Models.Articles
{
    public class DisplayArticleViewModel : IMapFrom<Article>
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageName { get; set; }

        public string UserId { get; set; }
    }
}
