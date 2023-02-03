using AutoMapper;
using NewsApp.Data.Models;
using NewsApp.Services.Mapping;

namespace NewsApp.Models.Comments
{
    public class DisplayCommentsViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public DisplayCommentsViewModel()
        {
            this.InnerComments = new List<DisplayCommentsViewModel>();
        }
        public string Id { get; set; }
        public string Content { get; set; }
        public string UserUserName { get; set; }
        public string CreatedOn { get; set; }
        public string OuterCommentId { get; set; }

        public ICollection<DisplayCommentsViewModel> InnerComments { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Comment, DisplayCommentsViewModel>()
                .ForMember(vm => vm.CreatedOn, opt =>
                {
                    opt.MapFrom(c => c.CreatedOn.ToString("g"));
                });
            configuration.CreateMap<Comment, DisplayCommentsViewModel>()
                .ForMember(vm => vm.OuterCommentId, opt =>
                {
                    opt.MapFrom(c => c.OuterCommentId == null ? null : c.OuterCommentId.ToString());
                });

        }
    }
}
