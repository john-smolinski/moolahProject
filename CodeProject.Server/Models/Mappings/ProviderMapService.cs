using CodeProject.Server.Context;
using CodeProject.Server.Models.Entities;

namespace CodeProject.Server.Models.Mappings
{
    public class ProviderMapService : IProviderMapService
    {
        private readonly MoolahContext _moolahContext;

        public ProviderMapService(MoolahContext moolahContext)
        {
            _moolahContext = moolahContext;
        }

        public Provider GetProviderByName(string providerName)
        {
            return _moolahContext.Providers.SingleOrDefault(p => p.Name == providerName)
                ?? throw new InvalidOperationException($"No provider with name {providerName} found");
        }
    }
}
