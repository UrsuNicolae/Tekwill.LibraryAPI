using AutoMapper;
using Library.Domain.Entities;

namespace Library.Aplication.DTOs.Books
{
    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
        {
            CreateMap<CreateBookDto, Book>()
                .ReverseMap();

            CreateMap<Book, BookDto>()
                .ForMember(s => s.Author, des => des.MapFrom(s => s.Author != null ? new Author(s.Author) : null))
                .ReverseMap();
        }
    }
}
