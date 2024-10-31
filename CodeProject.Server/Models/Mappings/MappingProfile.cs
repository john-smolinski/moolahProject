using AutoMapper;
using CodeProject.Server.Models.Dtos;
using CodeProject.Server.Models.Entities;

namespace CodeProject.Server.Models.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile(IServiceProvider serviceProvider)
        {
            // Initialize ProviderIdResolver with the provided service provider
            var providerIdResolver = new ProviderIdResolver(serviceProvider);

            // Map ToDo to ToDoDto
            CreateMap<ToDo, ToDoDto>()
                .ForMember(dest => dest.ProviderName,
                    opt => opt.MapFrom(src => src.Provider != null ? src.Provider.Name : null));

            // Map ToDoDto to ToDo using ProviderIdResolver for ProviderId
            CreateMap<ToDoDto, ToDo>()
                .ForMember(dest => dest.ProviderId, opt => opt.MapFrom(providerIdResolver))
                .ForMember(dest => dest.Provider, opt => opt.Ignore()); // Ignore Provider to avoid EF Core insert attempt
        }
    }
}