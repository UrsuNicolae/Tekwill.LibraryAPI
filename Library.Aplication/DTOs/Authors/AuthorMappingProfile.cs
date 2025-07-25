using AutoMapper;
using Library.Domain.Entities;

namespace Library.Aplication.DTOs.Authors
{
    public class AuthorMappingProfile : Profile
    {
        public AuthorMappingProfile()
        {
            CreateMap<CreateAuthorDto, Author>()
                .ReverseMap();
            CreateMap<Author, AuthorDto>()
                .ReverseMap();
        }
    }
}
