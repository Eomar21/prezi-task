using PreziViewer.Models;
using PreziViewer.Services.Interface;

namespace PreziViewer.Services
{
    internal class PresentationLocalFetcher : IPresentationLocalFetcher
    {
        public IEnumerable<Presentation> TryGetOnlinePresentations()
        {
            throw new NotImplementedException();
        }
    }
}
