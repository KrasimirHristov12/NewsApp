using AutoMapper;
using NewsApp.Data.Models;
using NewsApp.Services.Mapping;

namespace NewsApp.Models.Articles
{
    public class HomeArticlesViewModel : IMapFrom<Article>, IHaveCustomMappings
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Article, HomeArticlesViewModel>()
                .ForMember(vm => vm.ImageName, opt =>
                {
                    opt.MapFrom(a => a.Images.FirstOrDefault().Name);
                });
        }
    }
}
