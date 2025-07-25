using AutoMapper;
using Library.Domain.Entities;

namespace Library.Aplication.DTOs.Genres
{
    public class GenreMappingProfile : Profile
    {
        public GenreMappingProfile()
        {
            CreateMap<CreateGenreDto, Gen>()
                .ReverseMap();
            CreateMap<Gen, GenreDto>()
                .ReverseMap();
        }
    }
}
