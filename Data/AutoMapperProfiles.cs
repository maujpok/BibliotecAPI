using AutoMapper;
using BibliotecAPI.DTOs;
using BibliotecAPI.Entities;

namespace BibliotecAPI.Data
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CategoryEntity, CategoryDTO>()
                .ReverseMap();
            CreateMap<CategoryCreationDTO, CategoryEntity>();

            CreateMap<AuthorEntity, AuthorDTO>()
                .ReverseMap();
            CreateMap<AuthorCreationDTO, AuthorEntity>();

            CreateMap<BookEntity, BookDTO>()
                .ReverseMap();
            CreateMap<BookCreationDTO, BookEntity>();

            CreateMap<EditorialEntity, EditorialDTO>()
                .ReverseMap();
            CreateMap<EditorialCreationDTO, EditorialEntity>();
        }
    }
}