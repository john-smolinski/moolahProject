using AutoMapper;
using CodeProject.Server.Context;
using CodeProject.Server.Models.Dtos;
using CodeProject.Server.Models.Entities;

namespace CodeProject.Server.Models.Mappings
{
    public class ProviderIdResolver : IValueResolver<ToDoDto, ToDo, int>
    {
        private readonly IServiceProvider _serviceProvider;

        public ProviderIdResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public int Resolve(ToDoDto source, ToDo destination, int destMember, ResolutionContext context)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<MoolahContext>();

            // Find the Provider by name and return its Id
            var provider = dbContext.Providers.SingleOrDefault(p => p.Name == source.ProviderName);
            return provider?.Id ?? throw new Exception($"Provider with name '{source.ProviderName}' not found.");
        }
    }

}
