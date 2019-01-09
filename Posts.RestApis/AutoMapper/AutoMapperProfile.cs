using AutoMapper;
using Posts.Entities.Models;

namespace Posts.RestApis.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User,UserDTO>();
            CreateMap<UserDTO,User>();
        }
    }
}