using CodeProject.Server.Models.Entities;

namespace CodeProject.Server.Models.Mappings
{
    public interface IProviderMapService
    {
        Provider GetProviderByName(string providerName);
    }
}
