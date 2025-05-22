using AutoMapper;
using BibliotecaApi.DTOs;
using BibliotecaApi.Entities;

namespace BibliotecaApi.Utilities
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // For autores
            CreateMap<Autor, AutorDTO>()
                .ForMember(dto => dto.FullName,config => config.MapFrom(
                    autor => $"{autor.Names} {autor.LastNames}"));
            CreateMap<AutorCreacionDTO, Autor>();

            // For libros
            CreateMap<Libro, LibroDTO>();
            CreateMap<LibroCreacionDTO,  Libro>();
        }
    }
}
