using Newtonsoft.Json;

namespace PreziViewer.Models
{
    public class Owner
    {
        [JsonProperty("Id")]
        public Guid? Id { get; set; }

        [JsonProperty("FirstName")]
        public string? FirstName { get; set; }

        [JsonProperty("LastName")]
        public string? LastName { get; set; }
    }
}