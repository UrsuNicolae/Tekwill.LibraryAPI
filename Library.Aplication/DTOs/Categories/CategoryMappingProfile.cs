using AutoMapper;
using Library.Domain.Entities;

namespace Library.Aplication.DTOs.Categories
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, CategoryDto>()
                .ReverseMap();
            CreateMap<CreateCategoryDto, Category>();
        }
    }
}
