using Newtonsoft.Json;
using PreziViewer.Models;
using PreziViewer.Services.Interface;
using System.Diagnostics;

namespace PreziViewer.Services
{
    internal class PresentationOnlineFetcher : IPresentationOnlineFetcher
    {
        private readonly HttpClient m_HttpClient;

        public PresentationOnlineFetcher(HttpClient httpClient)
        {
            m_HttpClient = httpClient;
        }
        public async Task<Presentations> TryGetOnlinePresentations()
        {
            try
            {
                string url = "https://s3.amazonaws.com/prezi-desktop/other/Assesment/prezilist.json";
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
            SavePresentations(presentations, AppContext.BaseDirectory + "presentations.json"); // TODO : Move to configuration
            return presentations;
        }

        private void SavePresentations(Presentations presentations, string pathToSave)
        {
            // Save to local storage
            using (StreamWriter file = File.CreateText(pathToSave))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, presentations);
            }
        }
    }
}
