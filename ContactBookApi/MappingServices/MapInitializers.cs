using AutoMapper;
using Models.Entities;
using Shared.DTOs;
using Shared.DTOs.ContactDTOs;
using Shared.DTOs.UserDTOs;

namespace ContactBookApi.MappingServices
{
    public class MapInitializers:Profile
    {
        public MapInitializers()
        {
            CreateMap<UserDtoForCreation, User>();
            CreateMap<User, UserDisplayDto>();
            CreateMap<Contact, ContactDisplayDto>().ForMember(m=>m.PhotoUrl, o => o.MapFrom(src => src.Photos.FirstOrDefault().Url));
            CreateMap<ContactDtoForCreation, Contact>();
            CreateMap<ContactDtoForUpdate, Contact>();
        }
    }
}
