using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
