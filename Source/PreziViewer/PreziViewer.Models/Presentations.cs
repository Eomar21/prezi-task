using Newtonsoft.Json;

namespace PreziViewer.Models
{
    public class Presentations
    {
        [JsonProperty("presentations")]
        public IEnumerable<Presentation> List { get; }

        public Presentations(IEnumerable<Presentation> list)
        {
            List = list;
        }

        public Presentations()
        {
            List = new List<Presentation>();
        }

        public static Presentations Empty = new();
    }
}