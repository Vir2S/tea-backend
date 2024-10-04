using AutoMapper;
using Tea_Store.DTOs.UsersDTO;
using Tea_Store.Models;

namespace Tea_Store.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<RegistrationDTO, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
        }
    }
}
