using AutoMapper;
using PlayListProject.Application.Dtos;
using PlayListProject.Domain.Entities;

namespace PlayListProject.Application.Mappers
{
    public class MapperAuthor : Profile
    {
        public MapperAuthor()
        {
            CreateMap<DtoAuthor, Author>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(dto => dto.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(dto => dto.Name))
                .ReverseMap();
        }
    }
}
