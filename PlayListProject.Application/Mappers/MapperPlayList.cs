using AutoMapper;
using PlayListProject.Domain.Entities;
using PlayListProject.Application.Dtos;

namespace PlayListProject.Application.Mappers
{
    public class MapperPlayList: Profile
    {
        public MapperPlayList()
        {
            CreateMap<DtoPlayList, PlayList>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(dto => dto.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(dto => dto.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(dto => dto.Description))
                .ReverseMap();
        }
    }
}
