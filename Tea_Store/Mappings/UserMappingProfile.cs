using AutoMapper;
using Tea_Store.Models;
using ViewModels.AuthController;

namespace Tea_Store.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            /*
            CreateMap<RegistrationViewModel, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
            */
        }
    }
}
