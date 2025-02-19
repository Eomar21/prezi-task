using PreziViewer.Models;

namespace PreziViewer.Services.Interface
{
    public interface IPresentationLocalFetcher
    {
        Task<Presentations> TryGetLocalPresentations();
    }
}