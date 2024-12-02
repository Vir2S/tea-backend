using AutoMapper;
using Tea_Store.Models;
using ViewModels.TeaContoller;

namespace Tea_Store.Mappings
{
    public class TeaMappingProfile : Profile
    {
        public TeaMappingProfile()
        {
            CreateMap<Tea, TeaCatalogViewModel>();
        }
    }
}
