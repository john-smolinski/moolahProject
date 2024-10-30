using AutoMapper; 

namespace CodeProject.Server.Models
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
