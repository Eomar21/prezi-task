using PreziViewer.Models;

namespace PreziViewer.Services.Interface
{
    public interface IPresentationFetcher
    {
        IEnumerable<Presentation> GetPresentations();
    }
}