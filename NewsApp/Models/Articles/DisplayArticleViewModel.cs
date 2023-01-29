using AutoMapper;
using NewsApp.Data.Models;
using NewsApp.Models.Images;
using NewsApp.Services.Mapping;

namespace NewsApp.Models.Articles
{
    public class DisplayArticleViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public IEnumerable<ImagesViewModel> Images { get; set; }

        public string UserId { get; set; }


    }
}
