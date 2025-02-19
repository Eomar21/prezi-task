using Microsoft.Extensions.Configuration;
using PreziViewer.Services.Interface;
namespace PreziViewer.Services
{
    internal class ConfigurationService : IConfigurationService
    {
        public IConfigurationRoot Configuration { get; }
        public ConfigurationService()
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            Configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();
        }
        public string GetString(string key)
        {
            return Configuration[key] ?? string.Empty;
        }
    }
}
