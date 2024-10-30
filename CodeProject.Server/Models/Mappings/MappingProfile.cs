using AutoMapper;
using CodeProject.Server.Models.Dtos;
using CodeProject.Server.Models.Entities;

namespace CodeProject.Server.Models.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ToDo, ToDoDto>()
                .ForMember(dest => dest.PoviderName, opt => opt.MapFrom(src => src.Provider.Name));

            CreateMap<ToDoDto, ToDo>()
                .ForMember(dest => dest.Provider, opt => opt.Ignore())
                .AfterMap((src, dest) => dest.Provider = new Provider { Name = src.PoviderName });
        }
    }
}
