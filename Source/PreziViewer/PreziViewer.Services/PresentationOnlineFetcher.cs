using Newtonsoft.Json;
using PreziViewer.Models;
using PreziViewer.Services.Interface;
using System.Diagnostics;

namespace PreziViewer.Services
{
    internal class PresentationOnlineFetcher : IPresentationOnlineFetcher
    {
        private readonly HttpClient m_HttpClient;
        private readonly IConfigurationService m_ConfigurationService;

        public PresentationOnlineFetcher(HttpClient httpClient, IConfigurationService configurationService)
        {
            m_HttpClient = httpClient;
            m_ConfigurationService = configurationService;
        }

        public async Task<Presentations> TryGetOnlinePresentations()
        {
            try
            {
                string url = m_ConfigurationService.GetString(ConfigurationService.OnlineRepo);
                string json = await m_HttpClient.GetStringAsync(url);
                var presentations = JsonConvert.DeserializeObject<Presentations>(json);
                return presentations ?? Presentations.Empty;
            }
            catch (Exception e)
            {
                if (e.InnerException is HttpRequestException)
                {
                    Debug.WriteLine("Failed to fetch online presentations due to network error - Probably we are offline");
                }
                else
                {
                    Debug.WriteLine("Failed to fetch online presentations due to unknown error");
                }
                return Presentations.Empty;
            }
        }

        public async Task<Presentations> TryGetOnlinePresentationsAndSave()
        {
            var presentations = await TryGetOnlinePresentations();
            SavePresentations(presentations, AppContext.BaseDirectory + m_ConfigurationService.GetString(ConfigurationService.LoggingLocation));
            return presentations;
        }

        private void SavePresentations(Presentations presentations, string pathToSave)
        {
            using (StreamWriter file = File.CreateText(pathToSave))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, presentations);
            }
        }
    }
}