using Newtonsoft.Json;
using PreziViewer.Models;
using PreziViewer.Services.Interface;
using System.Diagnostics;

namespace PreziViewer.Services
{
    internal class PresentationLocalFetcher : IPresentationLocalFetcher
    {
        private readonly IConfigurationService m_ConfigurationService;

        public PresentationLocalFetcher(IConfigurationService configurationService)
        {
            m_ConfigurationService = configurationService;
        }

        public async Task<Presentations> TryGetLocalPresentations()
        {
            var pathTofile = AppContext.BaseDirectory + m_ConfigurationService.GetString(ConfigurationService.LoggingLocation);
            try
            {
                string fileContent = await File.ReadAllTextAsync(pathTofile);
                var presentations = JsonConvert.DeserializeObject<Presentations>(fileContent);
                return presentations ?? Presentations.Empty;
            }
            catch (Exception e)
            {
                if (e.InnerException is FileNotFoundException)
                {
                    Debug.WriteLine($"Failed to fetch local presentations due to file not found: {e.Message}");
                }
                else
                {
                    Debug.WriteLine($"Failed to fetch local presentations due to error: {e.Message}");
                }
                return Presentations.Empty;
            }
        }
    }
}