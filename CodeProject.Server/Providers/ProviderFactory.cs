using CodeProject.Server.Context;

namespace CodeProject.Server.Providers
{
    public class ProviderFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ProviderFactory(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public IProviderService GetToDoService(string providerName)
        { 
            if (providerName.Equals("home", StringComparison.OrdinalIgnoreCase)) 
            {
                return _serviceProvider.GetService(typeof(HomeService)) as IProviderService
                    ?? throw new InvalidOperationException("HomeService is not registered");
            }
            else
            {
                return _serviceProvider.GetService(typeof(OfficeService)) as IProviderService
                    ?? throw new InvalidOperationException("OfficeService is not registered");
            }
        }
    }
}
