using AutoMapper;
using Library.Domain.Entities;

namespace Library.Aplication.DTOs.Authors
{
    public class AuthorMappingProfile : Profile
    {
        public AuthorMappingProfile()
        {
            CreateMap<CreateAuthorDto, Author>()
                .ForMember(a => a.AuthorGenres, dest => dest.MapFrom(s => s.GenrIds.Select(g => new AuthorGenres(0, g))))
                .ReverseMap();
            CreateMap<AuthorDto, Author>()
                .ForMember(a => a.AuthorGenres, dest => dest.MapFrom(s => s.GenrIds.Select(g => new AuthorGenres(s.Id, g))))
                .ReverseMap();
        }
    }
}
