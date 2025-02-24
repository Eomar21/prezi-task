using Newtonsoft.Json;

namespace PreziViewer.Models
{
    public class Presentation
    {
        [JsonProperty("Id")]
        public Guid Id { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("ThumbnailUrl")]
        public Uri ThumbnailUrl { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Privacy")]
        public string Privacy { get; set; }

        [JsonProperty("LastModified")]
        public DateTime LastModified { get; set; }

        [JsonProperty("Owner")]
        public Owner Owner { get; set; }
    }
}