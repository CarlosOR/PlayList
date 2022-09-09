using AutoMapper;
using PlayListProject.Application.Dtos;
using PlayListProject.Domain.Entities;

namespace PlayListProject.Application.Mappers
{
    public class MapperSong : Profile
    {
        public MapperSong()
        {
            CreateMap<DtoSong, Song>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(dto => dto.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(dto => dto.Name))
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(dto => dto.AuthorId))
                .ReverseMap();
        }
    }
}
